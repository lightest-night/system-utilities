using System;
using System.Globalization;

namespace LightestNight.System.Utilities.Extensions
{
    public static class ExtendsDateTimeOffset
    {
        public static string Serialize(this DateTimeOffset target)
            => target.ToString("O", CultureInfo.InvariantCulture);
    }
}