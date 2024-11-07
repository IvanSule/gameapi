using FluentAssertions;
using NetArchTest.Rules;
using RPSSL.Domain.Primitives;
using System.Reflection;

namespace ArchitectureTests.Domain
{
    public class DomainTests : BaseTest
    {      
        [Fact]
        public void Entities_Should_BeSealed()
        {
            var testResult = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity))
                .Should()
                .BeSealed()
                .GetResult();

            testResult.IsSuccessful.Should().BeTrue();
        }

        [Fact]
        public void Entities_Should_HavePrivateParameterlessConstructor()
        {

            var entityTypes = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity))
                .GetTypes();

            var failingTypes = new List<Type>();

            foreach (var entityType in entityTypes)
            {
                var constructors = entityType.GetConstructors(BindingFlags.NonPublic | 
                    BindingFlags.Instance);
                if (!constructors.Any(ctr=>ctr.IsPrivate && ctr.GetParameters().Length == 0))
                {
                    failingTypes.Add(entityType);
                }                
            }

            failingTypes.Should().BeEmpty();
        }

        [Fact]
        public void Entities_Should_NotHaveFieldsWithPublicSetters()
        {

            var entityTypes = Types.InAssembly(DomainAssembly)
                .That()
                .Inherit(typeof(Entity))
                .GetTypes();

            var failingTypes = new List<Type>();

            foreach (var entityType in entityTypes)
            {
                var properties = entityType.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (properties.Any(field => field.GetSetMethod(true) != null && field.GetSetMethod(true)!.IsPublic))
                {
                    failingTypes.Add(entityType);
                }
            }

            failingTypes.Should().BeEmpty();
        }
    }
}