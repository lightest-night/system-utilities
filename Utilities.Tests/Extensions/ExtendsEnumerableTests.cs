using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightestNight.Utilities.Extensions;
using Shouldly;
using Xunit;

namespace LightestNight.Utilities.Tests.Extensions
{
    public class ExtendsEnumerableTests
    {
        [Fact]
        public void Should_Return_Null()
        {
            // Act
            #nullable disable
            var result = (null as IEnumerable<int>).IsNullOrEmpty();
            #nullable restore
            
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

        [Fact]
        public async Task Should_Iterate_Asynchronously()
        {
            // Arrange
            var enumerable = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var result = new List<int>();

            // Act 
            await enumerable.ForEach(i =>
            {
                result.Add(i + 1);
                return Task.CompletedTask;
            });
            
            // Assert
            result.ShouldBe(new List<int>{2, 3, 4, 5, 6, 7, 8, 9, 10, 11});
        }

        [Theory]
        [InlineData(5)]
        [InlineData(2)]
        [InlineData(10)]
        public async Task Should_Iterate_Asynchronously_With_DOP(int dop)
        {
            // Arrange
            var enumerable = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            var result = new List<int>();

            // Act 
            await enumerable.ForEach(i =>
            {
                result.Add(i + 1);
                return Task.CompletedTask;
            }, dop);
            
            // Assert
            result.ShouldBe(new List<int>{2, 3, 4, 5, 6, 7, 8, 9, 10, 11});
        }
    }
}