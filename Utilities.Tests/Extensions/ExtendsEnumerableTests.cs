using System.Collections.Generic;
using System.Linq;
using LightestNight.System.Utilities.Extensions;
using Shouldly;
using Xunit;

namespace LightestNight.System.Utilities.Tests.Extensions
{
    public class ExtendsEnumerableTests
    {
        [Fact]
        public void Should_Return_Null()
        {
            // Act
            var result = ((IEnumerable<int>) null).IsNullOrEmpty();
            
            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Should_Return_Empty()
        {
            // Act
            var result = Enumerable.Empty<int>().IsNullOrEmpty();
            
            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public void Should_Return_NotEmptyOrNull()
        {
            // Act
            var result = new List<int> {1, 2, 3}.IsNullOrEmpty();
            
            // Assert
            result.ShouldBeFalse();
        }
    }
}