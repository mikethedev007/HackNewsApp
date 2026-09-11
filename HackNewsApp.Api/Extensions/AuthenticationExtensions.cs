using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using HackNewsApp.Api.Authentication;
using System.Runtime.CompilerServices;

namespace HackNewsApp.Api.Extensions
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
            IConfiguration configuration)
        {
            //binding configuration
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

            var jwtOptions = configuration.GetSection(JwtOptions.SectionName)
                .Get<JwtOptions>() ??
                throw new InvalidOperationException("JWT configuration missing.");

            services.AddSingleton<JwtTokenGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    //setup JWT Bearer Token Security with values from config file
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            jwtOptions.SecretKey))
                    };
                });

            return services;
        }

    }
}
