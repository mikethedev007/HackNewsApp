using HackNewsApp.Api.Extensions;
using HackNewsApp.Api.ResultExecution;
using HackNewsApp.Application.Contracts.Repositories;
using HackNewsApp.Application.Contracts.ResultExecution;
using HackNewsApp.Application.Contracts.Services;
using HackNewsApp.Application.DependencyInjection;
using HackNewsApp.Application.Services;
using HackNewsApp.Infrastructure.DependencyInjection;
using HackNewsApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//set up environment config file
var env = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true);

// Add services to the container.

builder.Services.AddControllers();

//Api Versioning
builder.Services.AddApiVersioningConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Add Jwt Authentication and Policy Authorisation
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddPolicyAuthorisation();

builder.Services.AddSwaggerConfiguration();
builder.Services.AddRateLimiterConfiguration();
builder.Services.AddOpenTelemetryConfiguration();
//builder.Services.AddHealthChecks();

//Register classes
builder.Services.AddScoped<IResultExecutor, ResultExecutor>();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplicationRegistration();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(x =>
    {
        x.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
    });
}

app.UseRateLimiter();

//app.MapHealthChecks("/health");
//app.MapHealthChecks("/health/live");
//app.MapHealthChecks("/health/ready");


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();

app.Run();
