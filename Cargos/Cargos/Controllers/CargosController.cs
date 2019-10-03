using Cargos.API.DataContract;
using Cargos.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cargos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        private ICargoService CargoService { get; set; }

        public CargosController(ICargoService cargoService)
        {
            this.CargoService = cargoService;
        }

        #region POST
        [HttpPost]
        public IActionResult Post(EventoInputDataContract input)
        {
            if (!this.CargoService.CheckFormatEventoInput(input))
            {
                var errors = this.CargoService.GetErrorsCheckEvento(input);
                return BadRequest(errors);
            }

            var cargo = this.CargoService.CreateEvento(input);

            return StatusCode(StatusCodes.Status201Created, cargo);
        }
        #endregion

        #region PUT
        [HttpPut]
        public IActionResult Put(CargoUpdateDataContract input)
        {
            if (!this.CargoService.CheckFormatCargoUpdate(input))
            {
                var errors = this.CargoService.GetErrorsCheckCargoUpdate(input);
                return BadRequest(errors);
            }

            if (!this.CargoService.CheckExistCargo(input))
                return NotFound($"No existe cargo con id: {input.Cargo_Id}");

            if (!this.CargoService.CheckStateCargo(input))
                return BadRequest($"No existe deuda para el cargo con id: {input.Cargo_Id}");

            var output = this.CargoService.UpdateCargo(input);

            return Ok(output);
        }
        #endregion

        #region GET
        [HttpGet("[action]/{id}")]
        public IActionResult GetCargoById(long id)
        {
            var cargo = this.CargoService.GetCargoById(id);

            if (cargo == null)
                return NotFound($"No existe el cargo con id: {id}");

            return Ok(cargo);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetFacturaById(long id)
        {
            var factura = this.CargoService.GetFacturaById(id);

            if (factura == null)
                return NotFound($"No se encontro la factura con id: {id}");

            return Ok(factura);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetDebtByUser(long id)
        {
            var debt = this.CargoService.GetDeudaByUser(id);

            if (debt.Amount == 0)
                return NotFound($"No existe deuda para el usuario id: {id}");

            return Ok(debt);
        }
        #endregion
    }
}