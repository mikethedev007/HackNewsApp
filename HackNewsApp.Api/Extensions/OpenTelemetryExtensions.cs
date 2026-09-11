using OpenTelemetry.Resources;
using OpenTelemetry.Trace;


namespace HackNewsApp.Api.Extensions
{
    public static class OpenTelemetryExtensions
    {
        public static IServiceCollection AddOpenTelemetryConfiguration(this IServiceCollection services)
        {
            services.AddOpenTelemetry()
                    .WithTracing(builder =>
                    {
                        builder
                            .SetResourceBuilder(
                                ResourceBuilder.CreateDefault()
                                    .AddService("HackNewsApp"))

                            .AddAspNetCoreInstrumentation()
                            .AddHttpClientInstrumentation()

                        .AddSource("HackNewsApp");

                        //.AddOtlpExporter();
                    });

            return services;
        }
    }
}
