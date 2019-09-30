using Cargos.API.DataContract;
using Cargos.Infrastructure.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cargos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private ICargoRepository CargoRepository { get; set; }
        private IFacturaRepository FacturaRepository { get; set; }
        private IEventoRepository EventoRepository { get; set; }

        private IValidator<EventoInputDataContract> EventoInputDataContractValidator { get; set; }

        // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
        private const decimal CONVERSION_FACTOR = 60;

        public CargosController(ICargoRepository cargoRepository, IFacturaRepository facturaRepository, IEventoRepository eventoRepository, IValidator<EventoInputDataContract> eventoInputDataContractValidator)
        {
            CargoRepository = cargoRepository;
            FacturaRepository = facturaRepository;
            EventoRepository = eventoRepository;

            EventoInputDataContractValidator = eventoInputDataContractValidator;
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var cargo = this.CargoRepository.GetById(id);

            if (cargo != null)
                return Ok(cargo);

            return StatusCode(StatusCodes.Status404NotFound);
        }

        [HttpPost]
        public IActionResult Post(EventoInputDataContract input)
        {
            var validationResult = EventoInputDataContractValidator.Validate(input);

            if (!validationResult.IsValid)
            {
                IList<string> errores = new List<string>();

                foreach (var error in validationResult.Errors)
                    errores.Add(error.ToString());

                return BadRequest(errores);
            }

            Domain.Evento evento = CreateEvento(input);

            CheckBillCurrentPeriod(evento);

            CreateCargo(evento);

            return StatusCode(StatusCodes.Status201Created);
        }

        private Domain.Evento CreateEvento(EventoInputDataContract input)
        {
            Domain.Evento evento = new Domain.Evento()
            {
                Amount = input.Amount,
                Date = input.Date,
                User_Id = input.User_id,
                Event_Id = input.Event_id,
                Currency = (Domain.Currency)Enum.Parse(typeof(Domain.Currency), input.Currency),
                Type = (Domain.TypeEvento)Enum.Parse(typeof(Domain.TypeEvento), input.Event_type),
            };

            EventoRepository.Save(evento);

            return evento;
        }

        private void CheckBillCurrentPeriod(Domain.Evento evento)
        {
            Domain.Factura bill = GetBillByPeriodAndUser(evento);

            if (bill == null)
                CreateBill(evento);
        }

        private void CreateBill(Domain.Evento evento)
        {
            Domain.Factura bill = new Domain.Factura()
            {
                User_Id = evento.User_Id,
                Month = evento.Date.Month,
                Year = evento.Date.Year,
            };

            this.FacturaRepository.Save(bill);
        }

        private void CreateCargo(Domain.Evento evento)
        {
            decimal conversionFactor = 1;
            decimal amount;

            Domain.Factura bill = GetBillByPeriodAndUser(evento);

            // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
            if (evento.Currency != Domain.Currency.ARS)
                conversionFactor = CONVERSION_FACTOR;

            amount = evento.Amount * conversionFactor;

            Domain.Cargo cargo = new Domain.Cargo()
            {
                Amount = amount,
                Balance = amount,
                Event = evento,
                State = Domain.StateCargo.Deuda,
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

        private Domain.Factura GetBillByPeriodAndUser(Domain.Evento evento)
        {
            return this.FacturaRepository.GetAll().SingleOrDefault(x => x.Month == evento.Date.Month && x.Year == evento.Date.Year && x.User_Id == evento.User_Id);
        }
    }
}