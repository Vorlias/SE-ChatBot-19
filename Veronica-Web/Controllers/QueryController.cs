using Microsoft.AspNetCore.Mvc;
using RasaLib.Metadata;
using RasaLib.Rasa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static JsonResult Result(string response)
        {
            QueryResponse queryResponse = new QueryResponse() { Response = response };
            return new JsonResult(queryResponse);
        }
    }

    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        Papers papers = new Papers("Data/Papers.json");

        [HttpPost("[action]")]
        public JsonResult Ask([FromBody] QueryArguments query)
        {
            if (query.UserInput.StartsWith("@"))
            {
                RasaQuery rasaQuery = new RasaQuery(query.UserInput.Substring(1));
                var response = rasaQuery.GetResponse();

                return Json(new QueryResponse() { Response = response.GetDebug() });
            }
            else
            {
                RasaQuery rasaQuery = new RasaQuery(query.UserInput);
                var response = rasaQuery.GetResponse();

                if (response.Intent == null)
                {
                    return Json(new { response = "I have no idea what to respond with." });
                }

                if (response.Intent?.Name == RasaResponse.INTENT_REQUIREMENTS)
                {
                    var paperEntities = response.Entities.Where(entity => entity.Entity == RasaResponse.ENTITY_PAPER);
                    if (paperEntities.Count() > 0)
                    {
                        var paper = paperEntities.First();
                        var matchingPaper = papers.FindPaperByKeyword(paper.Value);
                        if (matchingPaper != null)
                        {
                            var req = string.Join(" or ", matchingPaper.Requirements);

                            var str = $"{matchingPaper.Name} ({matchingPaper.PaperCode}) requires that you have completed {req}";
                            return Json(new { Response = str });
                        }
                        else
                        {
                            return QueryResponse.Result("I don't know what paper you're asking about."); //Json(new { Response = "I don't know what paper you're asking about." });
                        }
                    }
                }
                else
                { 

                    return QueryResponse.Result($"I'm stupid and thought you are talking about {response.Intent.Name} with an accuracy of {(response.Intent.Confidence * 100).ToString("F2")}");
                }
            }

            return Json(new { response = "Um, idk." });
        }
    }
}
