using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargos.API.DataContract;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Interface;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            //var cargo = new CargoOutputDataContract() { Amount = 105, Balance = 5, State = "Pagado", Type = "Services", User_Id = 1 };

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
                {
                    errores.Add(error.ToString());
                }

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

                // cambiar por descripcion
                Type = Domain.TypeEvento.Clasificado,
                Currency = Domain.Currency.Dolar,
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
            Domain.Factura bill = GetBillByPeriodAndUser(evento);

            Domain.Cargo cargo = new Domain.Cargo()
            {
                Amount = evento.Amount,
                Balance = evento.Amount,
                Event = evento,
                State = Domain.StateCargo.Deuda,
                User_Id = evento.User_Id,

                // cambiar por descripcion
                Type = Domain.TypeCargo.Servicios,
            };

            this.CargoRepository.Save(cargo);

            bill.Cargos.Add(cargo);

            this.FacturaRepository.Update(bill);
        }

        private Domain.Factura GetBillByPeriodAndUser(Domain.Evento evento)
        {
            return this.FacturaRepository.GetAll().SingleOrDefault(x => x.Month == evento.Date.Month && x.Year == evento.Date.Year && x.User_Id == evento.User_Id);
        }
    }
}