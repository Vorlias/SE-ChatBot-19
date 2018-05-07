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
        public IRasaModel Model { get; }
        public string Query { get; }

        public UserQuery(IRasaModel model, string query)
        {
            Model = model;
            Query = query;
        }

        public RasaResponse Send()
        {

        }
    }
}
