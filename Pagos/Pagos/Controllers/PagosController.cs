using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pagos.API.DataContract;
using Pagos.API.Service;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
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
            string uriCargos = "https://localhost:44311/";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uriCargos);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Cargos/GetDebtByUser/51");

                if (response.IsSuccessStatusCode)
                {
                    var entity_Response = response.Content.ReadAsStringAsync().Result;

                    DebtInputDataContract debt = JsonConvert.DeserializeObject<DebtInputDataContract>(entity_Response);
                }
            }








                if (!this.PagoService.CheckPago(input))
                {
                    var errors = this.PagoService.GetErrorsCheckPago(input);
                    return BadRequest(errors);
                }

            var pago = this.PagoService.CreatePago(input);

            return CreatedAtAction(nameof(this.PagoService.GetPagoById), new { id = pago.Pago_Id }, pago);
        }

        #endregion

        #region GET
        #endregion
    }
}