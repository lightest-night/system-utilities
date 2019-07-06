using System.Linq;
using System.Reflection;
using LightestNight.System.Utilities.Extensions;
using Shouldly;
using Xunit;

namespace LightestNight.System.Utilities.Tests.Extensions
{
    public class ExtendsAssemblyTests
    {
        private interface ITestInterface{}

        private class TestClass : ITestInterface {}

        private readonly Assembly _assembly = Assembly.GetExecutingAssembly();

        [Fact]
        public void Should_Get_Instance_Type_When_Using_Generic_Method()
        {
            // Act
            var result = _assembly.GetInstancesOfInterface<ITestInterface>().ToArray();
            
            // Assert
            result.Length.ShouldBe(1);
            result.ShouldContain(t => t == typeof(TestClass));
        }

        [Fact]
        public void Should_Get_Instance_Type_When_Using_Absolute_Method()
        {
            // Act
            var result = _assembly.GetInstancesOfInterface(typeof(ITestInterface)).ToArray();
            
            // Assert
            result.Length.ShouldBe(1);
            result.ShouldContain(t => t == typeof(TestClass));
        }
    }
}