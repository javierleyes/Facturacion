using Microsoft.AspNetCore.Http;
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
            if (!this.PagoService.CheckInput(input))
            {
                var errors = this.PagoService.GetErrorsCheckPago(input);
                return BadRequest(errors);
            }




            string uri = "https://localhost:44311/";

            string action = "api/Cargos/GetDebtByUser/";
            string id = input.User_id.ToString();

            DebtInputDataContract debt = new DebtInputDataContract();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"{action}{id}");

                if (response.IsSuccessStatusCode)
                {
                    var entity_Response = response.Content.ReadAsStringAsync().Result;

                    debt = JsonConvert.DeserializeObject<DebtInputDataContract>(entity_Response);
                }
            }



            if (!this.PagoService.CheckAmountDebt(input, debt))
                return BadRequest("El monto del pago es superior al monto de la deuda.");

            var pago = this.PagoService.CreatePago(input, debt);

            return StatusCode(StatusCodes.Status201Created, pago);
        }

        #endregion

        #region GET
        #endregion
    }
}