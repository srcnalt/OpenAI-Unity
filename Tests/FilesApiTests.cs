using System;
using NUnit.Framework;
using System.Threading.Tasks;

namespace OpenAI.Tests
{
    public class FilesApiTests
    {
        private OpenAIApi openai = new OpenAIApi();
        private string createdFileId;
        
        [Test, Order(0)]
        public async Task Create_File()
        {
            var req = new CreateFileRequest
            {
                File = Environment.CurrentDirectory + "/Assets/Tests/Data/training.jsonl",
                Purpose = "fine-tune"
            };
            var file = await openai.CreateFile(req);
            Assert.NotNull(file.Id);

            createdFileId = file.Id;
        }
        
        [Test, Order(1)]
        public async Task List_Files()
        {
            var files = await openai.ListFiles();
            Assert.Greater(files.Data.Count, 0);
        }
        
        [Test, Order(2)]
        public async Task Retrieve_File()
        {
            var file = await openai.RetrieveFile(createdFileId);
            Assert.AreEqual(createdFileId, file.Id);
        }
        
        [Test, Order(3)]
        public async Task Retrieve_File_Content()
        {
            var res = await openai.DownloadFile(createdFileId);
            Assert.IsNotNull(res);
        }
        
        [Test, Order(4)]
        public async Task Delete_File()
        {
            var res = await openai.DeleteFile(createdFileId);
            Assert.AreEqual(createdFileId, res.Id);
            Assert.IsTrue(res.Deleted);
        }
    }
}
