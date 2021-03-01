using System;
using Shouldly;
using Xunit;

namespace LightestNight.Utilities.Tests
{
    public class AttributesTests
    {
        private class TestAttribute : Attribute
        {
            public string Property { get; }

            public TestAttribute(string property)
            {
                Property = property;
            }
        }

        private class OtherAttribute : Attribute
        {
            public string Property { get; set; } = string.Empty;
        }

        [Test("Property")]
        private class TestClass
        { }

        [Fact]
        public void Should_Get_Correct_Value_From_Attribute_Property()
        {
            // Act
            var result = Attributes.GetCustomAttributeValue<TestAttribute, string>(typeof(TestClass), attr => attr.Property);
            
            // Assert
            result.ShouldBe("Property");
        }

        [Fact]
        public void Should_Get_Default_Value_For_Type_When_Not_Given_And_Attribute_Is_Of_Wrong_Type()
        {
            // Act
            var result = Attributes.GetCustomAttributeValue<OtherAttribute, string>(typeof(TestClass), attr => attr.Property);
            
            // Assert
            result.ShouldBe(default);
        }

        [Fact]
        public void Should_Be_Given_Default_When_Attribute_Is_Of_Wrong_Type()
        {
            // Arrange
            const string defaultValue = "DefaultValue";
            
            // Act
            var result = Attributes.GetCustomAttributeValue<OtherAttribute, string>(typeof(TestClass), attr => attr.Property, defaultValue);
            
            // Assert
            result.ShouldBe(defaultValue);
        }
    }
}