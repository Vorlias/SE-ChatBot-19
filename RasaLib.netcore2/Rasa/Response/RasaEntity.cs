using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RasaLib.Rasa.Response
{
    /*
     
      "entities": [
    {
      "start": 41,
      "extractor": "ner_crf",
      "end": 47,
      "value": "school",
      "entity": "query"
    }
  ],
  */
    public static class RasaEntityExtras
    {
        public static bool ContainsWordsInEntity(this IEnumerable<RasaEntity> entities, string entityName, params string[] words)
        {
            var matchingEntities = entities.Where(ent => ent.Entity.Equals(entityName, StringComparison.CurrentCultureIgnoreCase));

            int count = 0;
            int maxCount = words.Length;
            foreach (var word in words)
            {
                var isValid = matchingEntities.Select(entity => entity.Value).Contains(word);
                if (isValid)
                    count++;
            }

            return count >= maxCount;
        }
    }

    public class RasaEntity
    {
        public string Extractor { get; set; }
        public int Start { get; set; }
        public int End { get; set; }

        public string Value { get; set; }
        public string Entity { get; set; }

        public override string ToString()
        {
            return $"[{Entity}@{Start}:{End}] \"{Value}\"";
        }
    }
}
