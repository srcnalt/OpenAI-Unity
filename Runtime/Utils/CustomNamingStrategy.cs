using System.Text.RegularExpressions;
using Newtonsoft.Json.Serialization;

namespace OpenAI
{ 
    public class CustomNamingStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
            var result = Regex.Replace(name, "([A-Z])", m => (m.Index > 0 ? "_" : "") + m.Value[0].ToString().ToLowerInvariant());
            return result;
        }
    }
}
