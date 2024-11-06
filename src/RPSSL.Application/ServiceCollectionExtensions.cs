using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RPSSL.Application.Behaviors;

namespace RPSSL.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var thisAssembly = typeof(ServiceCollectionExtensions).Assembly;

            services.AddMediatR(config =>
                       config.RegisterServicesFromAssembly(thisAssembly).AddOpenBehavior(typeof(ValidationBehavior<,>)));

            services.AddValidatorsFromAssembly(thisAssembly);

            return services;
        }
    }
}
