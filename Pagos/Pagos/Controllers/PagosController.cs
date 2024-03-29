﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pagos.API.DataContract;
using Pagos.API.Service;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Post(PagoInputDataContract input)
        {
            if (!this.PagoService.CheckInput(input))
            {
                var errors = this.PagoService.GetErrorsCheckPago(input);
                return BadRequest(errors);
            }

            DebtInputDataContract debt = await this.PagoService.GetDebtByUser(input.User_id);

            if (!this.PagoService.CheckAmountDebt(input, debt))
                return BadRequest("El monto del pago es superior al monto de la deuda.");

            var pago = await this.PagoService.CreatePago(input, debt);

            return StatusCode(StatusCodes.Status201Created, pago);
        }

        #endregion

        #region GET
        [HttpGet("[action]/{id}")]
        public IActionResult GetPagoById(long id)
        {
            var pago = this.PagoService.GetPagoById(id);

            if (pago == null)
                return NotFound($"No existe el pago con id: {id}");

            return Ok(pago);
        }

        [HttpGet("[action]/{id}")]
        public IActionResult GetPagoByUser(long id)
        {
            if (this.PagoService.UserExist(id) == false)
                return NotFound($"No existe el usuario id: {id}");

            var status = this.PagoService.GetPagoByUser(id);

            return Ok(status);
        }
        #endregion
    }
}