using System;
using LightestNight.System.Utilities.Extensions;
using Shouldly;
using Xunit;

namespace LightestNight.System.Utilities.Tests.Extensions
{
    public class ExtendsDateTimeOffsetTests
    {
        [Fact]
        public void ShouldSerializeUtcDateTimeToStringCorrectly()
        {
            // Arrange
            var utcDate = new DateTime(1982, 2, 8, 10, 30, 45, 45);
            utcDate = DateTime.SpecifyKind(utcDate, DateTimeKind.Utc);
            var utcDateOffset = new DateTimeOffset(utcDate);
            
            // Act
            var result = utcDateOffset.Serialize();
            
            // Assert
            result.ShouldBe("1982-02-08T10:30:45.0450000+00:00");
        }

        [Fact]
        public void ShouldContainAllRelevantDatePartsWhenSerializingUtcDateTime()
        {
            // Arrange
            var utcDate = DateTime.UtcNow;
            var utcDateOffset = new DateTimeOffset(utcDate);
            
            // Act
            var result = utcDateOffset.Serialize();
            
            // Assert
            // YYYY-MM-DD
            result.ShouldContain($"{utcDate.Year}-{FormatSingleDigitDateValue(utcDate.Month)}-{FormatSingleDigitDateValue(utcDate.Day)}");
            
            // THH:MM:SS
            result.ShouldContain($"T{FormatSingleDigitDateValue(utcDate.Hour)}:{FormatSingleDigitDateValue(utcDate.Minute)}:{FormatSingleDigitDateValue(utcDate.Second)}");
            
            // Ms7 0000000
            // Result should be 14 to include the . and the +/-00:00
            result.Substring(result.LastIndexOf('.')).Length.ShouldBe(14);
            
            // TZ
            result.ShouldEndWith("+00:00", Case.Sensitive);
        }
        
        [Fact]
        public void ShouldSerializeDateTimeToStringCorrectly()
        {
            // Arrange
            var utcDate = new DateTime(1982, 2, 8, 10, 30, 45, 45);
            var offset = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time").GetUtcOffset(utcDate);
            var tokyoDate = new DateTimeOffset(utcDate, offset);
            
            // Act
            var result = tokyoDate.Serialize();
            
            // Assert
            result.ShouldBe("1982-02-08T10:30:45.0450000+09:00");
        }

        [Fact]
        public void ShouldContainCorrectTimeZoneWhenSerializingDateTime()
        {
            // Arrange
            var utcDate = new DateTime(1982, 2, 8, 10, 30, 45, 45);
            var offset = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time").GetUtcOffset(utcDate);
            var tokyoDate = new DateTimeOffset(utcDate, offset);
            
            // Act
            var result = tokyoDate.Serialize();
            
            // Assert
            result.ShouldEndWith("+09:00", Case.Sensitive);
        }

        private static string FormatSingleDigitDateValue(int value)
            => value <= 9
                ? $"0{value}"
                : value.ToString();
    }
}