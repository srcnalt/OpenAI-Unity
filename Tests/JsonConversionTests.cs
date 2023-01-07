using Newtonsoft.Json;
using NUnit.Framework;
using Newtonsoft.Json.Serialization;

namespace OpenAI.Tests
{
    public class JsonConversionTests
    {
        private readonly JsonSerializerSettings customNamingStrategy = new JsonSerializerSettings()
        {
            ContractResolver = new DefaultContractResolver()
            {
                NamingStrategy = new CustomNamingStrategy()
            }
        };

        internal struct PascalCasePayload
        {
            public int MyVar;
            public int MySecondVar;
        }
        
        [Test]
        public void Serialize_Pascal_Case_Object_Fields_Into_Snake_Case_Json_Content()
        {
            var content = new PascalCasePayload
            {
                MyVar = 1,
                MySecondVar = 2
            };

            var payload = JsonConvert.SerializeObject(content, customNamingStrategy);
            
            Assert.IsTrue(payload.Contains("my_var"));
            Assert.IsTrue(payload.Contains("my_second_var"));
        }
        
        [Test]
        public void Deserialize_Snake_Case_Json_Content_Into_Pascal_Case_Object_Fields()
        {
            var json = "{\"my_var\": 1, \"my_second_var\": 2 }";

            var content = JsonConvert.DeserializeObject<PascalCasePayload>(json, customNamingStrategy);
            
            Assert.AreEqual(1, content.MyVar);
            Assert.AreEqual(2, content.MySecondVar);
        }
    }
}
