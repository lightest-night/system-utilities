using System;
using System.Globalization;
using Newtonsoft.Json;

namespace LightestNight.System.Utilities.Extensions
{
    public static class ExtendsString
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        public static object? ExtractObject(this string target)
        {
            var objectStartIndex = target.ThrowIfNull(nameof(target)).IndexOf('{', StringComparison.InvariantCultureIgnoreCase);
            var objectStr = target.Substring(
                objectStartIndex,
                (target.LastIndexOf('}') - objectStartIndex) + 1);

            return JsonConvert.DeserializeObject(objectStr, SerializerSettings);
        }

        public static DateTimeOffset ToDateTimeOffset(this string target)
            => DateTimeOffset.Parse(target, styles: DateTimeStyles.RoundtripKind);
    }
}