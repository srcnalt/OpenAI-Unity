using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OpenAI
{
    public class Configuration
    {
        public string ApiKey { get; }
        public string Organization { get; }
        
        public Configuration()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            var json = File.ReadAllText($"{path}/.openai/auth.json");
            var auth = JsonConvert.DeserializeObject<JToken>(json);

            if (auth != null)
            {
                ApiKey = auth["api_key"]!.Value<string>();
                Organization = auth["organization"]!.Value<string>();
            }
            else
            {
                throw new Exception("auth.json could not be found.");
            }
        }
    }
}
