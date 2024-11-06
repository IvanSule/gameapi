using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using RPSSL.Application.Abstractions;
using RPSSL.Domain.Abstractions;
using RPSSL.Domain.Implementations;
using RPSSL.Infrastructure.Repositories;
using RPSSL.Infrastructure.Services;


namespace RPSSL.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IScoresReadService, ScoresReadService>();
            services.AddScoped<IRandomChoiceService, RandomChoiceService>();

            services.AddHttpClient<IRandomNumberHttpService, RandomNumberHttpService>((serviceProvider, client) =>
            {
                var address = serviceProvider.GetRequiredService<IConfiguration>().GetValue("BoohmaRandomNumberService:Url", "default");
                client.BaseAddress = new Uri(address);
            }).AddResilienceHandler("custom-pipeline", builder =>
            {
                builder.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromSeconds(1),
                    BackoffType = DelayBackoffType.Linear,
                    UseJitter = true
                })
                .AddTimeout(TimeSpan.FromSeconds(5));

            });

            services.AddDbContext<ApplicationDbContext>(builder =>
            builder.UseNpgsql(configuration.GetConnectionString("Database")));

            return services;
        }
    }
}
