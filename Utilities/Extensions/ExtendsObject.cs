using Newtonsoft.Json;

namespace LightestNight.System.Utilities.Extensions
{
    public static class ExtendsObject
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public static string SerializeWithType(this object target)
            => JsonConvert.SerializeObject(target, SerializerSettings);
    }
}