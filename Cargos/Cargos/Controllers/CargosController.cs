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
            var factura = this.CargoService.GetBillById(id);

            if (factura == null)
                return NotFound($"No se encontro la factura con id: {id}");

            return Ok(factura);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetBillByUser(long id)
        {
            if (this.CargoService.UserExist(id) == false)
                return NotFound($"No existe el usuario id: {id}");

            var bills = this.CargoService.GetBillsByUser(id);

            return Ok(bills);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetDebtByUser(long id)
        {
            if (this.CargoService.UserExist(id) == false)
                return NotFound($"No existe el usuario id: {id}");

            var debt = this.CargoService.GetDebtByUser(id);

            return Ok(debt);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetStatusUser(long id)
        {
            if (this.CargoService.UserExist(id) == false)
                return NotFound($"No existe el usuario id: {id}");

            var status = this.CargoService.GetStatusUser(id);

            return Ok(status);
        }
        #endregion
    }
}