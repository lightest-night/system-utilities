using LightestNight.System.Utilities.Extensions;
using Shouldly;
using Xunit;

namespace LightestNight.System.Utilities.Tests.Extensions
{
    internal class TestObject
    {
        public string Foo => "Bar";
    }
    
    public class ExtendsObjectTests
    {
        [Fact]
        public void Should_Serialize_With_Type_Information()
        {
            // Arrange
            var obj = new TestObject();
            
            // Act
            var result = obj.SerializeWithType();
            
            // Assert
            result.ShouldContain("$type");
        }
    }
}