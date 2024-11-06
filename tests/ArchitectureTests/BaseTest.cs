using RPSSL.Application;
using RPSSL.Domain.Primitives;
using System.Reflection;

namespace ArchitectureTests
{
    public abstract class BaseTest
    {
        protected static readonly Assembly DomainAssembly = typeof(Entity).Assembly;

        protected static readonly Assembly ApplicationAssembly = typeof(ServiceCollectionExtensions).Assembly;
    }
}
