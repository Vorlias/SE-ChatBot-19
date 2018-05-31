using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RasaLib.Metadata
{
    /// <summary>
    /// A collection of papers
    /// </summary>
    public class PapersDatabase
    {
        List<Paper> papers;
        public IEnumerable<Paper> PaperList => papers;

        /// <summary>
        /// Finds a list of papers that match the job title specified
        /// </summary>
        /// <param name="jobTitle">The job title</param>
        /// <returns>Papers that match the job title</returns>
        public IEnumerable<Paper> FindPapersMatchingJob(string jobTitle)
        {
            List<Paper> matchingPapers = new List<Paper>();
            foreach (var paper in papers)
            {
                if (paper.Jobs.Contains(jobTitle.ToLower()))
                {
                    matchingPapers.Add(paper);
                }
            }

            return matchingPapers;
        }

        /// <summary>
        /// Finds a paper matching the specified keyword
        /// </summary>
        /// <param name="keyword">The keyword</param>
        /// <returns>A paper, if it matches otherwise null</returns>
        public Paper FindPaperByKeyword(string keyword)
        {
            var keywordLower = keyword.ToLower();
            var matchingPapers = papers.Where(paper => paper.FullName.ToLower() == keywordLower || 
                paper.Aliases.Contains(keywordLower) || 
                paper.PaperCode.ToLower() == keywordLower);

            if (matchingPapers.Count() > 0)
            {
                return matchingPapers.First();
            }

            return null;
        }

        /// <summary>
        /// Create a new PapersDatabase from a JSON file
        /// </summary>
        /// <param name="paperMetadataFile">The JSON file</param>
        public PapersDatabase(string paperMetadataFile)
        {
            papers = JArray.Parse(File.ReadAllText(paperMetadataFile)).ToObject<List<Paper>>();
        }
    }
}
