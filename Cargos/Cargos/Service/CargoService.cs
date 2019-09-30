using Cargos.API.DataContract;
using Cargos.Domain;
using Cargos.Infrastructure.Interface;
using FluentValidation;
using System;
using System.Linq;

namespace Cargos.API.Service
{
    public class CargoService : ICargoService
    {
        private ICargoRepository CargoRepository { get; set; }
        private IFacturaRepository FacturaRepository { get; set; }
        private IEventoRepository EventoRepository { get; set; }

        private IValidator<EventoInputDataContract> EventoInputDataContractValidator { get; set; }

        // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
        private const decimal CONVERSION_FACTOR = 60;

        public CargoService(ICargoRepository cargoRepository, IFacturaRepository facturaRepository, IEventoRepository eventoRepository, IValidator<EventoInputDataContract> eventoInputDataContractValidator)
        {
            CargoRepository = cargoRepository;
            FacturaRepository = facturaRepository;
            EventoRepository = eventoRepository;

            EventoInputDataContractValidator = eventoInputDataContractValidator;
        }

        public void CreateEvento(EventoInputDataContract input)
        {
            Evento evento = new Evento()
            {
                Amount = input.Amount,
                Date = input.Date,
                User_Id = input.User_id,
                Event_Id = input.Event_id,
                Currency = (Currency)Enum.Parse(typeof(Currency), input.Currency),
                Type = (TypeEvento)Enum.Parse(typeof(TypeEvento), input.Event_type),
            };

            EventoRepository.Save(evento);

            this.CheckBillCurrentPeriod(evento);

            this.CreateCargo(evento);

            //return evento;
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

        private void CreateCargo(Evento evento)
        {
            decimal conversionFactor = 1;
            decimal amount;

            Factura bill = GetBillByPeriodAndUser(evento);

            // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
            if (evento.Currency != Currency.ARS)
                conversionFactor = CONVERSION_FACTOR;

            amount = evento.Amount * conversionFactor;

            Cargo cargo = new Cargo()
            {
                Amount = amount,
                Balance = amount,
                Event = evento,
                State = StateCargo.Deuda,
                User_Id = evento.User_Id,
                Type = this.GetTypeCargoByEventType(evento),
            };

            this.CargoRepository.Save(cargo);

            bill.Cargos.Add(cargo);

            this.FacturaRepository.Update(bill);
        }

        private Domain.TypeCargo GetTypeCargoByEventType(Domain.Evento evento)
        {
            switch (evento.Type)
            {
                case Domain.TypeEvento.CLASIFICADO:
                case Domain.TypeEvento.VENTA:
                case Domain.TypeEvento.ENVIO:
                    return Domain.TypeCargo.MARKETPLACE;

                case Domain.TypeEvento.CREDITO:
                case Domain.TypeEvento.FIDELIDAD:
                case Domain.TypeEvento.PUBLICIDAD:
                    return Domain.TypeCargo.SERVICIOS;

                case Domain.TypeEvento.MERCADOPAGO:
                case Domain.TypeEvento.MERCADOSHOP:
                    return Domain.TypeCargo.EXTERNO;

                default:
                    return Domain.TypeCargo.INDEFINIDO;
            }
        }

        private Factura GetBillByPeriodAndUser(Evento evento)
        {
            return this.FacturaRepository.GetAll().SingleOrDefault(x => x.Month == evento.Date.Month && x.Year == evento.Date.Year && x.User_Id == evento.User_Id);
        }
    }
}
