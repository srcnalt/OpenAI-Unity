using System;
using NUnit.Framework;
using System.Threading.Tasks;
using UnityEngine;

namespace OpenAI.Tests
{
    public class AudioApiTests
    {
        private OpenAIApi openai = new OpenAIApi();
        
        [Test]
        public async Task Create_Audio_Transcriptions()
        {
            var req = new CreateAudioTranscriptionsRequest
            {
                File = Environment.CurrentDirectory + "/Assets/Tests/Data/recording_english.m4a",
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);
            Assert.NotNull(res);
            
            Debug.Log(res.Text);
        }
        
        [Test]
        public async Task Create_Audio_Transcriptions_From_Estonian()
        {
            var req = new CreateAudioTranscriptionsRequest
            {
                File = Environment.CurrentDirectory + "/Assets/Tests/Data/recording_estonian.m4a",
                Model = "whisper-1",
                Language = "et"
            };
            var res = await openai.CreateAudioTranscription(req);
            Assert.NotNull(res);
            
            Debug.Log(res.Text);
        }
        
        [Test]
        public async Task Create_Audio_Translation_From_Turkish()
        {
            var req = new CreateAudioTranslationRequest
            {
                File = Environment.CurrentDirectory + "/Assets/Tests/Data/recording_turkish.m4a",
                Model = "whisper-1",
            };
            var res = await openai.CreateAudioTranslation(req);
            Assert.NotNull(res);

            Debug.Log(res.Text);
        }
    }
}
