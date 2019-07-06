using System;
using LightestNight.System.Utilities.Generators;
using Shouldly;
using Xunit;

namespace LightestNight.System.Utilities.Tests.Generators
{
    public class GuidGeneratorTests
    {
        [Fact]
        public void Should_Generate_Time_Based_Guid()
        {
            // Act
            var result = GuidGenerator.GenerateTimeBasedGuid();
            
            // Assert
            result.ShouldNotBeNull();
        }

        [Fact]
        public void Should_Generate_Time_Based_Guid_Based_On_Given_Time()
        {
            // Arrange
            var dateTime = DateTime.UtcNow.AddDays(2);
            
            // Act
            var result = GuidGenerator.GenerateTimeBasedGuid(dateTime);
            
            // Assert
            GuidGenerator.GetUtcDateTime(result).ShouldBe(dateTime);
        }

        [Fact]
        public void Should_Return_Version_Appropriately()
        {
            // Arrange
            var guid = GuidGenerator.GenerateTimeBasedGuid();
            
            // Act
            var result = guid.GetVersion();
            
            // Assert
            result.ShouldBe(GuidVersion.TimeBased);
        }
    }
}