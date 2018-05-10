using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RasaLib.Rasa;

namespace RasaLib.Test
{
    [TestClass]
    public class RasaIntentTesting
    {
        [TestMethod]
        public void TestPaperRequirementsQuestion()
        {
            RasaQuery userQuery = new RasaQuery("what is required to do software engineering");
            var response = userQuery.GetResponse();
            Assert.IsTrue(response.Intent.Name == "paper_requirements");

            var papers = response.Entities.Where(entity => entity.Entity == "paper")
                .Select(entity => entity.Value);

            Assert.IsTrue(papers.Contains("software engineering"));
        }

        [TestMethod]
        public void TestJobSuggestionsIntent()
        {
            RasaQuery userQuery = new RasaQuery("i want to be a web developer");
            var response = userQuery.GetResponse();
            Assert.IsTrue(response.Intent.Name == "suggested_papers");

            var job = response.Entities.Where(entity => entity.Entity == "job")
                .Select(entity => entity.Value);

            Assert.IsTrue(job.Contains("web developer"));
        }
    }
}
