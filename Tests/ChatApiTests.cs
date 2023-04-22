using NUnit.Framework;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        
        [Test]
        public async Task Create_Chat_Completion_Stream()
        {
            bool responseReceived = false;
            float timeout = 10;
            float time = 0;
            
            var req = new CreateChatCompletionRequest
            {
                Model = "gpt-3.5-turbo",
                Messages = new List<ChatMessage>()
                {
                    new ChatMessage() { Role = "user", Content = "Hello!" }
                },
                Temperature = 0,
                Stream = true
            };
            
            openai.CreateChatCompletionAsync(req, null, () =>
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
