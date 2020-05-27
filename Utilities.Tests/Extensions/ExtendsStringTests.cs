using System;
using LightestNight.System.Utilities.Extensions;
using Shouldly;
using Xunit;

namespace LightestNight.System.Utilities.Tests.Extensions
{
    public class ExtendsStringTests
    {
        private class TestObject
        {
            public TestObject? InnerTestObject { get; set; }

            public override bool Equals(object? obj)
            {
                if (!(obj is TestObject))
                    return false;

                var to = (TestObject) obj;

                if (to.InnerTestObject == null)
                    return InnerTestObject == null;
                if (InnerTestObject == null)
                    return to.InnerTestObject == null;

                return to.InnerTestObject.Equals(InnerTestObject);
            }

            public override int GetHashCode()
            {
                // ReSharper disable once NonReadonlyMemberInGetHashCode
                return InnerTestObject?.GetHashCode() ?? 75;
            }
        }
        
        [Theory]
        [InlineData("{0}")]
        [InlineData("Start Before Object: {0}")]
        [InlineData("{0}Text After Object")]
        [InlineData("Both Start {0} And End")]
        public void Should_Extract_Object_Properly(string pattern)
        {
            // Arrange
            var testObject = new TestObject();
            var testString = string.Format(pattern, testObject.SerializeWithType());
            
            // Act
            var result = testString.ExtractObject();
            
            // Assert
            result.ShouldBe(testObject);
            result.ShouldBeOfType<TestObject>();
        }

        [Theory]
        [InlineData("{0}")]
        [InlineData("Start Before Object: {0}")]
        [InlineData("{0}Text After Object")]
        [InlineData("Both Start {0} And End")]
        public void Should_Extract_Nested_Object_Properly(string pattern)
        {
            // Arrange
            var testObject = new TestObject {InnerTestObject = new TestObject()};
            var testString = string.Format(pattern, testObject.SerializeWithType());
            
            // Act
            var result = testString.ExtractObject();
            
            // Assert
            result.ShouldBe(testObject);
            result.ShouldBeOfType<TestObject>();
        }

        [Fact]
        public void ShouldParseUtcTimeToDateTimeOffsetCorrectly()
        {
            // Arrange
            const string dateTime = "1982-02-08T10:30:45.0450000+00:00";
            
            // Act
            var result = dateTime.ToDateTimeOffset();
            
            // Assert
            result.Year.ShouldBe(1982);
            result.Month.ShouldBe(2);
            result.Day.ShouldBe(8);
            result.Hour.ShouldBe(10);
            result.Minute.ShouldBe(30);
            result.Second.ShouldBe(45);
            result.Millisecond.ShouldBe(45);
            result.Offset.ShouldBe(TimeSpan.Zero);
        }

        [Fact]
        public void ShouldParseToDateTimeOffsetCorrectly()
        {
            // Arrange
            const string dateTime = "1982-02-08T10:30:45.0450000+09:00";
            
            // Act
            var result = dateTime.ToDateTimeOffset();
            
            // Assert
            result.Year.ShouldBe(1982);
            result.Month.ShouldBe(2);
            result.Day.ShouldBe(8);
            result.Hour.ShouldBe(10);
            result.Minute.ShouldBe(30);
            result.Second.ShouldBe(45);
            result.Millisecond.ShouldBe(45);
            result.Offset.ShouldBe(new TimeSpan(9, 0, 0));
        }
    }
}