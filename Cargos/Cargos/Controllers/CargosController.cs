using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cargos.API.DataContract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cargos.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CargosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get(long id)
        {
            var cargo = new CargoOutputDataContract() { Amount = 105, Balance = 5, State = "Pagado", Type = "Services", User_Id = 1 };
            
            return Ok(cargo);
        }

        [HttpPost]
        public IActionResult Post(EventoInputDataContract input)
        {
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}