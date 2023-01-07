using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class CompletionsApiTests
    {
        private OpenAIApi openai = new OpenAIApi();
        
        [Test]
        public async Task Create_Completion()
        {
            var req = new CreateCompletionRequest
            {
                Model = "text-davinci-003",
                Prompt = "Say this is a test",
                MaxTokens = 7,
                Temperature = 0
            };
            var res = await openai.CreateCompletion(req);
            Assert.NotNull(res);
        }
    }
}
