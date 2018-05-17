using System;
using System.Collections.Generic;
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

        public Papers(string paperMetadataFile)
        {

        }
    }
}
