using Newtonsoft.Json.Linq;
using RasaLib.Rasa.Response;
using System.Text;

namespace RasaLib.Rasa
{
    /// <summary>
    /// The response from RASA
    /// </summary>
    public class RasaResponse
    {
        public const string INTENT_SUGGESTION = "suggested_papers";
        public const string INTENT_REQUIREMENTS = "paper_requirements";
        public const string INTENT_AVAILABILITY = "availability";
        public const string ENTITY_PAPER = "paper";
        public const string ENTITY_JOB = "job";
        public const string ENTITY_MAJOR = "major";

        public RasaEntity[] Entities { get; }

        /// <summary>
        /// The text the user sent
        /// </summary>
        public string UserQuery { get; }

        /// <summary>
        /// The rankings of the possible intents
        /// </summary>
        public RasaIntent[] IntentRankings { get; }

        /// <summary>
        /// The calculated intent
        /// </summary>
        public RasaIntent Intent { get; }

        public string GetDebug()
        {
            return JObject.FromObject(this).ToString();
        }

        internal RasaResponse(RasaHttpResponseData data)
        {
            UserQuery = data.Text;
            IntentRankings = data.Intent_Ranking;
            Intent = data.Intent;
            Entities = data.Entities;
        }
    }
}