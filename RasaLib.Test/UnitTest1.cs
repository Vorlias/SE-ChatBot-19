using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RasaLib.Rasa;

namespace RasaLib.Test
{
    public class RasaTutorialModel : IRasaModel
    {
        public string Name => "rasaTutorialBot";
    }

    [TestClass]
    public class RasaIntentTesting
    {
        [TestMethod]
        public void TestBasicIntent()
        {
            RasaQuery userQuery = new RasaQuery(new RasaTutorialModel(), "hello rasa");
            var response = userQuery.GetResponse();
            
        }
    }
}
