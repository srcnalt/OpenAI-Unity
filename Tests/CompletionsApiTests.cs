using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class CompletionsApiTests
    {
        private OpenAIApi openai = new OpenAIApi();
        
        [Test]
        public async Task Create_Text_Completion()
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
        
        [Test]
        public async Task Create_Text_Completion_Stream()
        {
            bool responseReceived = false;
            float timeout = 5;
            float time = 0;
            
            var req = new CreateCompletionRequest
            {
                Model = "text-davinci-003",
                Prompt = "Say this is a test",
                MaxTokens = 7,
                Temperature = 0,
                Stream = true
            };
            
            openai.CreateCompletionAsync(req, null, () => 
            {
                responseReceived = true;
            }, new CancellationTokenSource());
            
            while (!responseReceived && time < timeout)
            {
                await Task.Delay(100);
                time += 0.1f;
            }
            
            Assert.IsTrue(responseReceived);
        }
    }
}
