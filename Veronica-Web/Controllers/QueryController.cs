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
    /// <summary>
    /// API end-point for api/Query/*
    /// </summary>
    [Route("api/[controller]")]
    public class QueryController : Controller
    {
        Papers papers = new Papers("Data/Papers.json");

        /// <summary>
        /// Gets a result for paper suggestions
        /// </summary>
        /// <param name="response">The response from RASA</param>
        /// <returns>A user-friendly response based on the RASA response</returns>
        private JsonResult GetPaperSuggestionResult(RasaResponse response)
        {
            // TODO: Implement
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a result for paper requirements
        /// </summary>
        /// <param name="response">The response from RASA</param>
        /// <returns>A user-friendly response based on the RASA response</returns>
        private JsonResult GetPaperRequirementResult(RasaResponse response)
        {
            // Get the entities that match 'paper'
            var paperEntities = response.Entities.Where(entity => entity.Entity == RasaResponse.ENTITY_PAPER);
            if (paperEntities.Count() > 0)
            {
                // Get the first entity, then find it in our paper data
                var paper = paperEntities.First();
                var matchingPaper = papers.FindPaperByKeyword(paper.Value);


                if (matchingPaper != null) // If we have a matching paper
                {
                    var requirementsString = string.Join(" or ", matchingPaper.Requirements);

                    // Respond with the requirements for the paper
                    return QueryResponse.Result($"{matchingPaper.Name} ({matchingPaper.PaperCode}) requires that you have completed {requirementsString}");
                }
                else  // If we don't have the paper in our data, inform the user.
                {

                    return QueryResponse.Result("I'm sorry, I don't have any information regarding that as of yet.");
                }
            }
            else
                return QueryResponse.Result("I'm sorry, could you ask that again?");
        }

        /// <summary>
        /// API end-point for api/Query/Ask (POST)
        /// - Accepts JSON
        /// - Must be in QueryArguments format
        /// </summary>
        /// <param name="query">The query arguments</param>
        /// <returns>A response from the query</returns>
        [HttpPost("[action]")]
        public JsonResult Ask([FromBody] QueryArguments query)
        {
            if (query.UserInput.StartsWith("@")) // Enable us to debug the bot in case it's displaying incorrect messages
            {
                RasaQuery rasaQuery = new RasaQuery(query.UserInput.Substring(1));
                RasaResponse rasaResponse = rasaQuery.GetResponse();

                return QueryResponse.Result(rasaResponse.GetDebug());
            }
            else // Regular messages
            {
                RasaQuery rasaQuery = new RasaQuery(query.UserInput);
                RasaResponse rasaResponse = rasaQuery.GetResponse();


                if (rasaResponse.Intent == null)  // If for some reason our bot decides there's no intent, we respond gracefully.
                {
                    return QueryResponse.Result("I'm only able to answer questions in relations to papers at AUT.");
                }


                if (rasaResponse.Intent?.Name == RasaResponse.INTENT_REQUIREMENTS) // Check if our intent is paper_requirements
                {
                    return GetPaperRequirementResult(rasaResponse);
                }
                else if (rasaResponse.Intent?.Name == RasaResponse.INTENT_SUGGESTION) // Else if our intent is a paper suggestion
                {
                    return GetPaperSuggestionResult(rasaResponse);
                }
                else // If our intent isn't supported we need to let the user know
                {

                    return QueryResponse.Result("I do not yet have the capability to answer that question.");
                }
            }

            // The last resort response (Should never be reached, hopefully)
            return QueryResponse.Result("I'm sorry, could you be more clear with what you're asking?");
        }
    }
}
