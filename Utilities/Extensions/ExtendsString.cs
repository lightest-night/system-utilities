using Newtonsoft.Json;

namespace LightestNight.System.Utilities.Extensions
{
    public static class ExtendsString
    {
        public static T ExtractObject<T>(this string target)
        {
            var objectStartIndex = target.IndexOf('{');
            var objectStr = target.Substring(
                objectStartIndex,
                (target.LastIndexOf('}') - objectStartIndex) + 1);

            return JsonConvert.DeserializeObject<T>(objectStr);
        }
    }
}