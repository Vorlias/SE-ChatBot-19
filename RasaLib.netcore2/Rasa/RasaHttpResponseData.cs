using Newtonsoft.Json.Linq;
using RasaLib.Rasa.Response;

namespace RasaLib.Rasa
{
    internal struct RasaHttpResponseData
    {
        /// <summary>
        /// The calculated intent
        /// </summary>
        public RasaIntent Intent { get; set; }

        /// <summary>
        /// Entities
        /// </summary>
        public RasaEntity[] Entities { get; set; }

        /// <summary>
        /// The rankings of the possible intents
        /// </summary>
        public RasaIntent[] Intent_Ranking { get; set; }

        /// <summary>
        /// The text the user sent
        /// </summary>
        public string Text { get; set; }
    }
}