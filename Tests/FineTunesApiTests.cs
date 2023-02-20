using System;
using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class FineTunesApiTests
    {
        private OpenAIApi openai = new OpenAIApi();
        private string createdFineTuneId;
        private string createdFineTunedModel;
        private string trainingFileId;
        
        // Unity Test Framework 1.3.2 does not have async OneTimeSetup and OneTimeTearDown methods.
        
        private async Task DeleteTrainingFile()
        {
            await openai.DeleteFile(trainingFileId);
        }

        private async Task CreateTrainingFile()
        {
            var req = new CreateFileRequest
            {
                File = Environment.CurrentDirectory + "/Assets/Tests/Data/training.jsonl",
                Purpose = "fine-tune",
            };
            var file = await openai.CreateFile(req);
            trainingFileId = file.Id;
        }

        [Test, Order(0)]
        public async Task Create_FineTune()
        {
            await CreateTrainingFile();
                
            var req = new CreateFineTuneRequest()
            {
                TrainingFile = trainingFileId
            };
            var res = await openai.CreateFineTune(req);
            Assert.IsNotNull(res);

            createdFineTuneId = res.Id;
            createdFineTunedModel = res.FineTunedModel;

            await DeleteTrainingFile();
        }
        
        [Test, Order(3)]
        public async Task Cancel_FineTune()
        {
            await CreateTrainingFile();
            
            var req = new CreateFineTuneRequest()
            {
                TrainingFile = trainingFileId
            };
            var fineTune = await openai.CreateFineTune(req);
            Assert.AreEqual("pending", fineTune.Status);
            
            var res = await openai.CancelFineTune(fineTune.Id);
            Assert.AreEqual("cancelled", res.Status);

            await DeleteTrainingFile();
        }

        [Test, Order(1)]
        public async Task List_FineTunes()
        {
            var fineTunes = await openai.ListFineTunes();
            Assert.Greater(fineTunes.Data.Count, 0);
        }
        
        [Test, Order(2)]
        public async Task Retrieve_FineTune()
        {
            var res = await openai.RetrieveFineTune(createdFineTuneId);
            Assert.AreEqual(createdFineTuneId, res.Id);
        }
        
        [Test, Order(4)]
        public async Task List_FineTune_Events()
        {
            var res = await openai.ListFineTuneEvents(createdFineTuneId);
            Assert.Greater(res.Data.Count, 0);
        }
        
        [Test, Order(5)]
        public async Task Delete_FineTuned_Model()
        {
            if(createdFineTunedModel != null)
            {
                var res = await openai.DeleteFineTunedModel(createdFineTunedModel);
                Assert.IsTrue(res.Deleted);
                Assert.AreEqual(createdFineTunedModel, res.Id);
            }
            else
            {
                Assert.True(true, "Skip test, fine-tuned model is either not ready or already deleted.");
            }
        }
    }
}
