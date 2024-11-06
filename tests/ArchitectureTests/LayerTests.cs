using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests
{
    public class LayerTests : BaseTest
    {
        [Fact]
        public void DomainLayer_Should_NotHaveDependencyOnApplicationLayer()
        {
            var testResult = Types.InAssembly(DomainAssembly)
                .Should()
                .NotHaveDependencyOn("RPSSL.Application")
                .GetResult();

            testResult.IsSuccessful.Should().BeTrue();  
        }
    }
}
