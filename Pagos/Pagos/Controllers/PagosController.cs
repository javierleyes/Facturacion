using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pagos.API.DataContract;
using Pagos.API.Service;

namespace Pagos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagosController : ControllerBase
    {
        private IPagoService PagoService { get; set; }

        public PagosController(IPagoService pagoService)
        {
            this.PagoService = pagoService;
        }

        #region POST

        [HttpPost]
        public IActionResult Post(PagoInputDataContract input)
        {
            if (!this.PagoService.CheckPago(input))
            {
                var errors = this.PagoService.GetErrorsCheckPago(input);
                return BadRequest(errors);
            }

            this.PagoService.CreatePago(input);

            return StatusCode(StatusCodes.Status201Created);
        }

        #endregion

        #region GET
        #endregion
    }
}