using NetArchTest.Rules;
using FluentAssertions;

namespace ArchitectureTests.Application
{
    public class ApplicationTests : BaseTest
    {
        [Fact]
        public void Application_Should_NotReferenceEntityFrameworkCore()
        {
            var testResult = Types.InAssembly(ApplicationAssembly)
                .That()
                .ResideInNamespaceContaining("RPSSL.Application")
                .ShouldNot()
                .HaveDependencyOn("Microsoft.EntityFrameworkCore")
                .GetResult();

            testResult.IsSuccessful.Should().BeTrue();
        }
    }
}
