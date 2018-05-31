using Microsoft.AspNetCore.Mvc;
using RasaLib.Metadata;
using RasaLib.Rasa;
using RasaLib.Rasa.Response;
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
        PapersDatabase papers = new PapersDatabase("Data/Papers.json");

        /// <summary>
        /// Gets a result for paper suggestions
        /// </summary>
        /// <param name="response">The response from RASA</param>
        /// <returns>A user-friendly response based on the RASA response</returns>
        private JsonResult GetPaperSuggestionResult(RasaResponse response)
        {
            // get the entities that match a job
            var jobs = response.Entities.Where(entity => entity.Entity == RasaResponse.ENTITY_JOB);
            if (jobs.Count() > 0)
            {
                var matchingPapers = papers.FindPapersMatchingJob(jobs.First().Value);
                if (matchingPapers.Count() > 0)
                {
                    string str = $"Papers matching '{jobs.First().Value}'";
                    foreach (var paper in matchingPapers)
                    {
                        str += $"\n{paper.FullName} ({paper.PaperCode})";
                    }
                    return QueryResponse.Result(str);
                }
                else
                    return QueryResponse.Result("No papers in my database match that job.");
            }
            else
                return QueryResponse.Result("I'm sorry, I didn't quite get that...");
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
                    var requirementsString = string.Join(" or ", matchingPaper.RequiredPapers);

                    // Respond with the requirements for the paper
                    return QueryResponse.Result($"{matchingPaper.FullName} ({matchingPaper.PaperCode}) requires that you have completed {requirementsString}");
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

                RasaIntent intent = rasaResponse.Intent;

                if (intent?.Name == RasaResponse.INTENT_REQUIREMENTS && intent.Confidence >= 0.5) // Check if our intent is paper_requirements
                {
                    return GetPaperRequirementResult(rasaResponse);
                }
                else if (intent?.Name == RasaResponse.INTENT_SUGGESTION && intent.Confidence >= 0.5) // Else if our intent is a paper suggestion
                {
                    return GetPaperSuggestionResult(rasaResponse);
                }
                else // If our intent isn't supported we need to let the user know
                {

                    return QueryResponse.Result("I do not yet have the capability to answer that question.");
                }
            }
        }
    }
}
