using Newtonsoft.Json.Linq;
using RasaLib.Rasa.Response;

namespace RasaLib.Rasa
{
    public class RasaResponse
    {
        public RasaIntent Intent { get; internal set;}
        public JObject Entities { get; internal set; }
    }
}