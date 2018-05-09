using RasaLib.Rasa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotPrototypeCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            do
            {
                Console.Write("Input> ");
                input = Console.ReadLine();

                if (input != string.Empty)
                {
                    RasaQuery rasaQuery = new RasaQuery("aut", input);
                    var result = rasaQuery.GetResponse();
                    Console.WriteLine("Got: {0} @{1}% confidence", result.Intent.Name, result.Intent.Confidence * 100f);
                    foreach (var entity in result.Entities)
                    {
                        Console.WriteLine("\t{0}", entity);
                    }

                    Console.WriteLine("Other potential results:  ");
                    foreach (var intent in result.IntentRankings)
                    {
                        Console.WriteLine("\tGot: {0} @{1}% confidence", intent.Name, intent.Confidence * 100);
                        foreach (var entity in result.Entities)
                        {
                            Console.WriteLine("\t\t{0}", entity);
                        }
                    }
                }
            }
            while (input != string.Empty);
        }
    }
}
