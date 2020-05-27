using System;
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
        public void ShouldSerializeWithTypeInformation()
        {
            // Arrange
            var obj = new TestObject();
            
            // Act
            var result = obj.SerializeWithType();
            
            // Assert
            result.ShouldContain("$type");
        }

        [Fact]
        public void ShouldThrowIfNull()
        {
            // Arrange
            TestObject? obj = null;
            
            // Act/Assert
            Should.Throw<ArgumentNullException>(() => obj.ThrowIfNull(nameof(obj)));
        }

        [Fact]
        public void ShouldThrowIfNullAndIncludeParameterName()
        {
            // Arrange
            TestObject? obj = null;
            
            // Act
            var exception = Should.Throw<ArgumentNullException>(() => obj.ThrowIfNull(nameof(obj)));
            
            // Assert
            exception.ParamName.ShouldBe(nameof(obj));
        }

        [Fact]
        public void ShouldNotThrowIfNotNull()
        {
            // Arrange
            var obj = new TestObject();
            
            // Act/Assert
            Should.NotThrow(() => obj.ThrowIfNull(nameof(obj)));
        }
    }
}