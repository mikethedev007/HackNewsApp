using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace HackNewsApp.Api.Extensions
{
    public static class RateLimiterExtensions
    {
        public static IServiceCollection AddRateLimiterConfiguration(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddFixedWindowLimiter(
                    "ApiLimiter",
                    config =>
                    {
                        config.PermitLimit = 100;
                        config.Window = TimeSpan.FromMinutes(1);
                        config.QueueLimit = 0;
                        config.AutoReplenishment = true;
                    });
            });

            return services;
        }
    }
}
