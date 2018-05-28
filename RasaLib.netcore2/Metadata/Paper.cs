using System;
using System.Text;
using System.Threading.Tasks;

namespace RasaLib.Metadata
{
    /// <summary>
    /// A paper offered in the degree
    /// </summary>
    public class Paper
    {
        /// <summary>
        /// The full name of the paper
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Any aliases the paper might be under
        /// </summary>
        public string[] Aliases { get; set; }

        /// <summary>
        /// The requirements for the paper
        /// </summary>
        public string[] RequiredPapers { get; set; }

        /// <summary>
        /// The paper's code
        /// </summary>
        public string PaperCode { get; set; }

        /// <summary>
        /// Any jobs that would match this paper
        /// </summary>
        public string[] Jobs { get; set; }

        /// <summary>
        /// Create a new paper
        /// </summary>
        /// <param name="paperCode">The paper's code</param>
        /// <param name="name">The name of the paper</param>
        /// <param name="aliases">Other names the paper may have</param>
        public Paper(string paperCode, string name, params string[] aliases)
        {
            FullName = name;
            PaperCode = paperCode;
            Aliases = aliases;
        }
    }
}
