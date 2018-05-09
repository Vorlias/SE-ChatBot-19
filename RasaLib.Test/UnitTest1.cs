using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RasaLib.Rasa;

namespace RasaLib.Test
{
    [TestClass]
    public class RasaIntentTesting
    {
        [TestMethod]
        public void TestBasicIntent()
        {
            RasaQuery userQuery = new RasaQuery("hello rasa");
            var response = userQuery.GetResponse();
            
        }
    }
}
