using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RasaLib.Rasa.Response
{
    /// <summary>
    /// The calculated intent from RASA
    /// </summary>
    public class RasaIntent
    {
        /// <summary>
        /// The confidence that this intent matches
        /// </summary>
        public double Confidence { get; internal set; }

        /// <summary>
        /// The name of the intent
        /// </summary>
        public string Name { get; set; }
    }
}
