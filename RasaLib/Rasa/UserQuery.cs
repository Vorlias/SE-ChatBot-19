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
    public class UserQuery
    {
        const string API_URL = "http://localhost:5000";

        public IRasaModel Model { get; }
        public string Query { get; }

        public UserQuery(IRasaModel model, string query)
        {
            Model = model;
            Query = query;
        }

        public RasaResponse Send()
        {
            JObject response = Http.PostJson($"{API_URL}/parse", new QueryData { Q = Query, Model = Model.Name });
        }
    }
}
