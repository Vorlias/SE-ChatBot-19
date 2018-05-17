using RasaLib.Rasa.Response;

namespace RasaLib.Rasa
{
    /// <summary>
    /// The response from RASA
    /// </summary>
    public class RasaResponse
    {

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

        internal RasaResponse(RasaHttpResponseData data)
        {
            UserQuery = data.Text;
            IntentRankings = data.Intent_Ranking;
            Intent = data.Intent;
            Entities = data.Entities;
        }
    }
}