using BloodPressure.Application.Common.Interfaces;
using BloodPressure.Domain.Models;
using BloodPressure.Infrastructure;
using BloodPressure.Infrastructure.Services;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace BloodPressure.Functions
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration? Configuration { get; private set; }

        private void InitializeConfiguration(IFunctionsHostBuilder builder)
        {
            var executionContextOptions = builder
                .Services
                .BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>()
                ?.Value;

            Configuration = new ConfigurationBuilder()
                .SetBasePath(executionContextOptions?.AppDirectory)
                .AddJsonFile("local.settings.json", false, true)
                .AddEnvironmentVariables()
                .Build();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            InitializeConfiguration(builder);

            builder.Services.AddOptions<ApplicationOptions>()
                .Configure<IConfiguration>((settings, _) =>
                    Configuration?.GetSection(nameof(ApplicationOptions)).Bind(settings));

            builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
            builder.Services.AddScoped<IDateTime, DateTimeService>();
            builder.Services.AddScoped<IBloodPressureService, BloodPressureService>();

            builder.Services.AddScoped<IBloodPressureDbContext>(x =>
            {
                var dateTime = x.GetRequiredService<IDateTime>();
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                var opt = new DbContextOptionsBuilder<BloodPressureDbContext>();
                opt.UseSqlServer(connectionString);
                return new BloodPressureDbContext(opt.Options, dateTime);
            });

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Worker", LogEventLevel.Warning)
                .MinimumLevel.Override("Host", LogEventLevel.Warning)
                .MinimumLevel.Override("Function", LogEventLevel.Warning)
                .MinimumLevel.Override("Azure", LogEventLevel.Warning)
                .MinimumLevel.Override("DurableTask", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.ApplicationInsights(
                    TelemetryConfiguration.CreateDefault(),
                    TelemetryConverter.Events,
                    LogEventLevel.Information)
                .CreateLogger();
            builder.Services.AddLogging(configure => configure.AddSerilog(Log.Logger));
        }
    }
}
