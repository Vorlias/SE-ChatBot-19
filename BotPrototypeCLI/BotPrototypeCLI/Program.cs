using RasaLib.Rasa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPrototypeCLI
{
    class Paper
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

    class Program
    {
        static Paper[] papersTest = new Paper[] {
            new Paper("ENSE701", "Contemporary Methods in Software Engineering", "Software Engineering") {Requirements=new string[]{ "COMP603" } },
            new Paper("COMP603", "Program Design and Construction", "PDC")
        };

        static void WriteOutput(string message)
        {
            var oldColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Veronica: {0}", message);
            Console.ForegroundColor = oldColor;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello there!");
            string input = string.Empty;
            do
            {
                Console.Write("# ");
                input = Console.ReadLine();

                if (input != string.Empty)
                {
                    RasaQuery rasaQuery = new RasaQuery(input);
                    var result = rasaQuery.GetResponse();

                    if (result.Intent.Name == "paper_requirements")
                    {
                        var papers = result.Entities.Where(entity => entity.Entity == "paper").Select(entity => entity.Value);
                        if (papers.Contains("software engineering"))
                        {
                            WriteOutput("To do Contemporary Methods in Software Engineering, you are required to do either COMP603, COMP610 or ENSE600");
                        }
                        
                    }


                    //if (result.Intent != null)
                    //    Console.WriteLine("Got: {0} @{1}% confidence", result.Intent.Name, result.Intent?.Confidence * 100f);
                    //else
                    //    Console.WriteLine("Got no intents.");

                    //foreach (var entity in result.Entities)
                    //{
                    //    Console.WriteLine("\t{0}", entity);
                    //}

                    //Console.WriteLine("Other potential results:  ");
                    //foreach (var intent in result.IntentRankings)
                    //{
                    //    Console.WriteLine("\tGot: {0} @{1}% confidence", intent.Name, intent.Confidence * 100);
                    //    foreach (var entity in result.Entities)
                    //    {
                    //        Console.WriteLine("\t\t{0}", entity);
                    //    }
                    //}
                }
            }
            while (input != string.Empty);
        }
    }
}
