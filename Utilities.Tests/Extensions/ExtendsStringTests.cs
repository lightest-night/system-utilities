using LightestNight.System.Utilities.Extensions;
using Shouldly;
using Xunit;

namespace LightestNight.System.Utilities.Tests.Extensions
{
    public class ExtendsStringTests
    {
        internal class TestObject
        {
            private static string Foo => "Foo";
            private static string Bar => "Bar";
            
            public TestObject InnerTestObject { get; set; }

            public override bool Equals(object obj)
            {
                if (!(obj is TestObject))
                    return false;

                var to = (TestObject) obj;

                if (to.InnerTestObject == null)
                    return InnerTestObject == null;

                return to.InnerTestObject.Equals(InnerTestObject);
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
            var testObject = new TestObject
            {
                InnerTestObject = new TestObject()
            };
            var testString = string.Format(pattern, testObject.SerializeWithType());
            
            // Act
            var result = testString.ExtractObject();
            
            // Assert
            result.ShouldBe(testObject);
            result.ShouldBeOfType<TestObject>();
        }
    }
}