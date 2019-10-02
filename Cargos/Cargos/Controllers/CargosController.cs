using Cargos.API.DataContract;
using Cargos.API.Service;
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
            if (!this.CargoService.CheckEvento(input))
            {
                var errors = this.CargoService.GetErrorsCheckEvento(input);
                return BadRequest(errors);
            }

            var cargo = this.CargoService.CreateEvento(input);

            return CreatedAtAction(nameof(this.CargoService.GetCargoById), new { id = cargo.Cargo_Id }, cargo);
        }

        #endregion

        #region GET

        [HttpGet("[action]/{id}")]
        public IActionResult GetCargoById(long id)
        {
            var cargo = this.CargoService.GetCargoById(id);

            if (cargo == null)
                return NotFound($"No se encontro el cargo con id: {id}");

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

        #endregion
    }
}