using System;
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

        public static T ThrowIfNull<T>(this T target, string? memberName = null)
        {
            if (target != null) 
                return target;
            
            if (memberName == null)
                throw new ArgumentNullException();
                
            throw new ArgumentNullException(memberName);
        }
    }
}