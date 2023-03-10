using System.Collections.Generic;
using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class ChatApiTests
    {
        private OpenAIApi openai = new OpenAIApi();
        
        [Test]
        public async Task Create_Chat_Completion()
        {
            var req = new CreateChatCompletionRequest
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<ChatMessage>()
                {
                    new ChatMessage() { Role = "user", Content = "Hello!" }
                }
            };
            var res = await openai.CreateChatCompletion(req);
            Assert.NotNull(res);
        }
    }
}
