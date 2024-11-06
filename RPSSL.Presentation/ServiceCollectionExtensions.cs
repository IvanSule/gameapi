using Microsoft.Extensions.DependencyInjection;
using RPSSL.Presentation.Exceptions;

namespace RPSSL.Presentation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            
            return services;
        }
    }
}
