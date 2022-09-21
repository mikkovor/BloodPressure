using BloodPressure.Application.Common.Interfaces;
using BloodPressure.Infrastructure;
using BloodPressure.Infrastructure.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        }
    }
}
