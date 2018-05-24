using Microsoft.AspNetCore.Mvc;
using RasaLib.Rasa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veronica_Web.Controllers
{
    public class QueryArguments
    {
        public string UserInput { get; set; }
    }

    public class QueryResponse
    {
        public string Response { get; set; }
    }

    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        [HttpPost("[action]")]
        public JsonResult Test(QueryArguments arguments)
        {
            return Json(new { Response = arguments.ToString() });
        }

        [HttpPost("[action]")]
        public JsonResult Ask([FromBody] QueryArguments query)
        {
            if (query.UserInput.StartsWith("@"))
            {
                RasaQuery rasaQuery = new RasaQuery(query.UserInput.Substring(1));
                var response = rasaQuery.GetResponse();

                return Json(new QueryResponse() { Response = response.GetDebug() });
            }

            return Json(new { response = "I don't work yet." });
        }
    }
}
