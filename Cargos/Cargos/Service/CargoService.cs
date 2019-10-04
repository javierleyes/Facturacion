using Cargos.API.DataContract;
using Cargos.Domain;
using Cargos.Infrastructure.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cargos.API.Service
{
    public class CargoService : ICargoService
    {
        private const string SIN_DEUDA = "El usuario no tiene cargos pendientes.";
        private const string CON_DEUDA = "El usuario tiene deudas pendientes.";

        private ICargoRepository CargoRepository { get; set; }
        private IFacturaRepository FacturaRepository { get; set; }
        private IEventoRepository EventoRepository { get; set; }

        private IValidator<EventoInputDataContract> EventoInputDataContractValidator { get; set; }
        private IValidator<CargoUpdateDataContract> CargoUpdateDataContractValidator { get; set; }

        // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
        private const decimal CONVERSION_FACTOR = 60;

        public CargoService(ICargoRepository cargoRepository, IFacturaRepository facturaRepository, IEventoRepository eventoRepository,
            IValidator<EventoInputDataContract> eventoInputDataContractValidator, IValidator<CargoUpdateDataContract> cargoUpdateDataContractValidator)
        {
            this.CargoRepository = cargoRepository;
            this.FacturaRepository = facturaRepository;
            this.EventoRepository = eventoRepository;

            this.EventoInputDataContractValidator = eventoInputDataContractValidator;
            this.CargoUpdateDataContractValidator = cargoUpdateDataContractValidator;
        }

        #region POST
        public bool CheckFormatEventoInput(EventoInputDataContract input)
        {
            return this.EventoInputDataContractValidator.Validate(input).IsValid;
        }

        public IList<string> GetErrorsCheckEvento(EventoInputDataContract input)
        {
            IList<string> errors = new List<string>();

            var validationResult = this.EventoInputDataContractValidator.Validate(input);

            foreach (var error in validationResult.Errors)
                errors.Add(error.ToString());

            return errors;
        }

        public CargoOutputDataContract CreateEvento(EventoInputDataContract input)
        {
            Evento evento = new Evento()
            {
                Amount = input.Amount,
                Date = input.Date,
                User_Id = input.User_id,
                Currency = (Currency)Enum.Parse(typeof(Currency), input.Currency),
                Type = (TypeEvento)Enum.Parse(typeof(TypeEvento), input.Event_type),
            };

            EventoRepository.Save(evento);

            this.CheckBillCurrentPeriod(evento);

            long idCargo = this.CreateCargo(evento);

            return this.GetCargoById(idCargo);
        }

        private void CheckBillCurrentPeriod(Evento evento)
        {
            Factura bill = GetBillByPeriodAndUser(evento);

            if (bill == null)
                CreateBill(evento);
        }

        private void CreateBill(Evento evento)
        {
            Factura bill = new Factura()
            {
                User_Id = evento.User_Id,
                Month = evento.Date.Month,
                Year = evento.Date.Year,
            };

            this.FacturaRepository.Save(bill);
        }

        private long CreateCargo(Evento evento)
        {
            Factura bill = GetBillByPeriodAndUser(evento);

            decimal payment_Amount = GetLegalAmount(evento.Currency.ToString(), evento.Amount);

            Cargo cargo = new Cargo()
            {
                Amount = payment_Amount,
                Balance = payment_Amount,
                Evento = evento,
                State = StateCargo.Deuda,
                User_Id = evento.User_Id,
                Type = this.GetTypeCargoByEventType(evento),
            };

            bill.Cargos.Add(cargo);

            this.FacturaRepository.Update(bill);

            return cargo.Id;
        }

        private TypeCargo GetTypeCargoByEventType(Evento evento)
        {
            switch (evento.Type)
            {
                case TypeEvento.CLASIFICADO:
                case TypeEvento.VENTA:
                case TypeEvento.ENVIO:
                    return TypeCargo.MARKETPLACE;

                case TypeEvento.CREDITO:
                case TypeEvento.FIDELIDAD:
                case TypeEvento.PUBLICIDAD:
                    return TypeCargo.SERVICIOS;

                case TypeEvento.MERCADOPAGO:
                case TypeEvento.MERCADOSHOP:
                    return TypeCargo.EXTERNO;

                default:
                    return TypeCargo.INDEFINIDO;
            }
        }

        private Factura GetBillByPeriodAndUser(Evento evento)
        {
            return this.FacturaRepository.GetAll().SingleOrDefault(x => x.Month == evento.Date.Month && x.Year == evento.Date.Year && x.User_Id == evento.User_Id);
        }

        private decimal GetLegalAmount(string currency, decimal amount_currency)
        {
            if ((Currency)Enum.Parse(typeof(Currency), currency) != Currency.ARS)
                return amount_currency * CONVERSION_FACTOR;

            return amount_currency;
        }
        #endregion

        #region GET
        public CargoOutputDataContract GetCargoById(long id)
        {
            Cargo cargo = this.CargoRepository.GetById(id);

            if (cargo == null)
                return null;

            return new CargoOutputDataContract()
            {
                Cargo_Id = cargo.Id,
                Amount = cargo.Amount,
                Balance = cargo.Balance,
                State = cargo.State.ToString(),
                Type = cargo.Type.ToString(),
                User_Id = cargo.User_Id,
            };
        }

        public FacturaOutputDataContract GetFacturaById(long id)
        {
            Factura bill = this.FacturaRepository.GetById(id);

            if (bill == null)
                return null;

            return new FacturaOutputDataContract()
            {
                Factura_Id = bill.Id,
                User_Id = bill.User_Id,
                Month = bill.Month,
                Year = bill.Year,
                Cargos_Id = bill.Cargos.Select(x => x.Id).ToList(),
            };
        }

        public DeudaUsuarioOutputDataContract GetDeudaByUser(long id)
        {
            IList<Cargo> cargos = this.CargoRepository.GetAll().Where(x => x.User_Id == id && x.State == StateCargo.Deuda).ToList();

            if (cargos == null)
                return null;

            DeudaUsuarioOutputDataContract debt = new DeudaUsuarioOutputDataContract();
            debt.Amount = cargos.Sum(x => x.Balance);

            foreach (Cargo cargo in cargos)
            {
                CargoOutputDataContract cargo_Output = new CargoOutputDataContract()
                {
                    Cargo_Id = cargo.Id,
                    Amount = cargo.Amount,
                    Balance = cargo.Balance,
                    State = cargo.State.ToString(),
                    Type = cargo.Type.ToString(),
                    User_Id = cargo.User_Id,
                };

                debt.Cargos.Add(cargo_Output);
            }

            return debt;
        }

        public UserOutputDataContract GetStatusUser(long id)
        {
            UserOutputDataContract user = new UserOutputDataContract
            {
                User_Id = id,
            };

            DeudaUsuarioOutputDataContract debt = this.GetDeudaByUser(id);

            if (debt == null)
                return null;

            if (debt.Amount == 0)
                user.State = SIN_DEUDA;
            else
                user.State = CON_DEUDA;

            return user;
        }

        public bool UserExist(long id)
        {
            return this.CargoRepository.GetAll().Any(x => x.User_Id == id);
        }
        #endregion

        #region PUT
        public CargoOutputDataContract UpdateCargo(CargoUpdateDataContract cargo_Update)
        {
            Cargo cargo = this.CargoRepository.GetById(cargo_Update.Cargo_Id);
            cargo.Balance -= cargo_Update.Payment_Debt;

            if (cargo.Balance == 0)
                cargo.State = StateCargo.Pagado;

            this.CargoRepository.Update(cargo);

            return new CargoOutputDataContract()
            {
                Cargo_Id = cargo.Id,
                Amount = cargo.Amount,
                Balance = cargo.Balance,
                State = cargo.State.ToString(),
                Type = cargo.Type.ToString(),
                User_Id = cargo.User_Id,
            };
        }

        public bool CheckFormatCargoUpdate(CargoUpdateDataContract cargo)
        {
            return this.CargoUpdateDataContractValidator.Validate(cargo).IsValid;
        }

        public IList<string> GetErrorsCheckCargoUpdate(CargoUpdateDataContract cargo)
        {
            IList<string> errors = new List<string>();

            var validationResult = this.CargoUpdateDataContractValidator.Validate(cargo);

            foreach (var error in validationResult.Errors)
                errors.Add(error.ToString());

            return errors;
        }

        public bool CheckStateCargo(CargoUpdateDataContract cargo_Update)
        {
            Cargo cargo = this.CargoRepository.GetById(cargo_Update.Cargo_Id);

            if (cargo.State == StateCargo.Deuda)
                return true;

            return false;
        }

        public bool CheckExistCargo(CargoUpdateDataContract cargo_Update)
        {
            Cargo cargo = this.CargoRepository.GetById(cargo_Update.Cargo_Id);

            if (cargo == null)
                return false;

            return true;
        }
        #endregion
    }
}
