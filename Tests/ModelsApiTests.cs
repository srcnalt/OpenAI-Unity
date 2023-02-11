using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public sealed class ModelsApiTests
    {
        private OpenAIApi openai = new OpenAIApi();

        [Test]
        public async Task List_Models()
        {
            var models = await openai.ListModels();
            Assert.Greater(models.Data.Count, 0);
        }

        [Test]
        public async Task Retrieve_Models()
        {
            var id = "text-davinci-003";
            var models = await openai.RetrieveModel(id);
            Assert.AreEqual(id, models.Id);
        }
    }
}
