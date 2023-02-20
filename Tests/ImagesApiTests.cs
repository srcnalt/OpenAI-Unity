using System;
using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class ImagesApiTests
    {
        private OpenAIApi openai = new OpenAIApi();

        [Test]
        public async Task Create_Image()
        {
            var req = new CreateImageRequest
            {
                Prompt = "A cute baby sea otter",
                N = 1,
                Size = "256x256",
            };
            var res = await openai.CreateImage(req);
            Assert.NotNull(res.Data);
        }
        
        [Test]
        public async Task Create_Image_Edit()
        {
            var req = new CreateImageEditRequest
            {
                Image = Environment.CurrentDirectory + "/Assets/Tests/Data/pool_empty.png",
                Mask = Environment.CurrentDirectory + "/Assets/Tests/Data/pool_flamingo.png",
                Prompt = "flamingo",
                N = 1,
                Size = "256x256",
            };
            var res = await openai.CreateImageEdit(req);
            Assert.NotNull(res.Data);
        }

        [Test]
        public async Task Create_Image_Variation()
        {
            var req = new CreateImageVariationRequest
            {
                Image = Environment.CurrentDirectory + "/Assets/Tests/Data/pool_empty.png",
                N = 1,
                Size = "256x256",
            };
            var res = await openai.CreateImageVariation(req);
            Assert.NotNull(res.Data);
        }
    }
}
