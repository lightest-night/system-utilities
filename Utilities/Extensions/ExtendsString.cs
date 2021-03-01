using Newtonsoft.Json;

namespace LightestNight.Utilities.Extensions
{
    public static class ExtendsString
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static object? ExtractObject(this string target)
        {
            var objectStartIndex = target.IndexOf('{');
            var objectStr = target.Substring(
                objectStartIndex,
                (target.LastIndexOf('}') - objectStartIndex) + 1);

            return JsonConvert.DeserializeObject(objectStr, SerializerSettings);
        }
    }
}