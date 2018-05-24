using Microsoft.AspNetCore.Mvc;
using RasaLib.Rasa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veronica_Web.Controllers
{
    [Route("api/[controller]")]
    public class RawDataController : Controller
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("Worked");
        }

        [HttpGet("[action]")]
        public IActionResult Query(string query)
        {
            RasaQuery rasaQuery = new RasaQuery(query);
            var response = rasaQuery.GetResponse();

            return Ok(response);
        }
    }
}
