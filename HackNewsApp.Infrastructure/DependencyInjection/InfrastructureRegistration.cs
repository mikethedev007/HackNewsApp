using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HackNewsApp.Application.Contracts.Caching;
using HackNewsApp.Application.Contracts.Repositories;
using HackNewsApp.Infrastructure.Caching;
using HackNewsApp.Infrastructure.Policies;
using HackNewsApp.Infrastructure.Repositories;
using HackNewsApp.Infrastructure.Http;
using Microsoft.Extensions.Options;

namespace HackNewsApp.Infrastructure.DependencyInjection
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.AddSingleton<ICacheService, CacheService>();
            services.AddScoped<IHackNewsRepository, HackNewsRepository>();

            services.Configure<ExternalApiOptions>(configuration.GetSection(ExternalApiOptions.SectionName));

            services
                .AddHttpClient<IHackNewsRepository, HackNewsRepository>((provider, client) =>
                {
                    var options = provider
                    .GetRequiredService<IOptions<ExternalApiOptions>>().Value;

                    client.BaseAddress = new Uri(configuration["ExternalApi:BaseUrl"]!);

                    client.Timeout = TimeSpan.FromSeconds(30);
                })
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
                .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());

            return services;
        }

    }
}
