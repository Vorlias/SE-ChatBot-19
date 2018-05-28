using Microsoft.AspNetCore.Mvc;

namespace Veronica_Web.Controllers
{
    /// <summary>
    /// A response for user querys
    /// </summary>
    public class QueryResponse
    {
        /// <summary>
        /// The response message
        /// </summary>
        public string Response { get; set; }

        /// <summary>
        /// Generate a response as a JsonResult
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static JsonResult Result(string response)
        {
            QueryResponse queryResponse = new QueryResponse() { Response = response };
            return new JsonResult(queryResponse);
        }
    }
}
