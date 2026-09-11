using HackNewsApp.Api.Authorisation;

namespace HackNewsApp.Api.Extensions
{
    public static class AuthorisationExtensions
    {
        public static IServiceCollection AddPolicyAuthorisation(this IServiceCollection services)
        {
            services.AddAuthorization(options => {
                options.AddPolicy(AuthorisationPolicies.CanViewHackNews,
                    policy =>
                    {
                        policy.RequireAuthenticatedUser();
                    });

                options.AddPolicy(AuthorisationPolicies.AdminOnly,
                    policy =>
                    {
                        policy.RequireRole("Admin");
                    });
            });

            return services;
        }

    }
}
