using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.Extensions.DependencyInjection;
using HackNewsApp.Application.Contracts.Services;
using HackNewsApp.Application.Services;
using HackNewsApp.Application.Validators;

namespace HackNewsApp.Application.DependencyInjection
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationRegistration(this IServiceCollection services)
        {
            services.AddScoped<IHackNewsService, HackNewsService>();

            services.AddValidatorsFromAssemblyContaining<HackNewsQueryRequestValidator>();

            return services;
        }
    }
}
