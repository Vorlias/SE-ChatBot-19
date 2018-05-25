using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RasaLib.Metadata
{
    public class Paper
    {
        public string Name { get; set; }
        public string[] Aliases { get; set; }
        public string[] Requirements { get; set; }
        public string PaperCode { get; set; }

        public Paper(string paperCode, string name, params string[] aliases)
        {
            Name = name;
            PaperCode = paperCode;
            Aliases = aliases;
        }
    }

    public class Papers
    {
        List<Paper> papers;
        public IEnumerable<Paper> PaperList => papers;

        public Paper FindPaperByKeyword(string keyword)
        {
            var keywordLower = keyword.ToLower();
            papers.Where(paper => paper.Name.ToLower() == keywordLower || 
                paper.Aliases.Contains(keywordLower) || 
                paper.PaperCode.ToLower() == keywordLower);

            if (papers.Count() > 0)
            {
                return papers.First();
            }

            return null;
        }

        public Papers(string paperMetadataFile)
        {
            papers = JArray.Parse(File.ReadAllText(paperMetadataFile)).ToObject<List<Paper>>();
            
        }
    }
}
