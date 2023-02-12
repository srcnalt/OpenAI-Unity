using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class EmbeddingsApiTests
    {
        private OpenAIApi openai = new OpenAIApi();

        [Test]
        public async Task Create_Embeddings()
        {
            var req = new CreateEmbeddingsRequest
            {
                Model="text-embedding-ada-002",
                Input="The food was delicious and the waiter..."
            };
            var res = await openai.CreateEmbeddings(req);
            Assert.Greater(res.Data.Count, 0);
        }
    }
}
