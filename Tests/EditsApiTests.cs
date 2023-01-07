using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class EditsApiTests
    {
        private OpenAIApi openai = new OpenAIApi();
 
        [Test]
        public async Task Create_Edit()
        {
            var req = new CreateEditRequest
            {
                Model = "text-davinci-edit-001",
                Input = "What day of the wek is it?",
                Instruction = "Fix the spelling mistakes",
            };
            var res = await openai.CreateEdit(req);
            Assert.AreEqual("What day of the week is it?\n", res.Choices[0].Text);
        }
    }
}
