using Newtonsoft.Json.Linq;
using RasaLib.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RasaLib.Rasa
{
    /// <summary>
    /// An object that represents a query by a user
    /// </summary>
    public class RasaQuery
    {
        const string API_URL = "http://localhost:5000";

        public string Model { get; }
        public string Query { get; }

        public RasaQuery(string model, string query)
        {
            Model = model;
            Query = query;
        }

        public RasaResponse GetResponse(string uri = API_URL)
        {
            JObject response = Http.PostJson($"{uri}/parse", new QueryData { q = Query, model = Model });
            RasaHttpResponseData data = response.ToObject<RasaHttpResponseData>();
            return new RasaResponse(data);
        }
    }
}
