using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public sealed class ModerationsApiTests
    {
        private OpenAIApi openai = new OpenAIApi();

        [Test]
        public async Task Create_Moderation()
        {
            var req = new CreateModerationRequest
            {
                Input = "I want to kill them."
            };
            var res = await openai.CreateModeration(req);
            Assert.IsTrue(res.Results[0].Flagged);
        }
    }
}
