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
            if (!this.CargoService.CheckEvento(input))
            {
                var errors = this.CargoService.GetErrorsCheckEvento(input);
                return BadRequest(errors);
            }

            this.CargoService.CreateEvento(input);

            return StatusCode(StatusCodes.Status201Created);
        }

        #endregion

        #region GET

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(long id)
        {
            var cargo = this.CargoService.GetById(id);

            if (cargo == null)
                return StatusCode(StatusCodes.Status404NotFound);

            return Ok(cargo);
        }

        #endregion
    }
}