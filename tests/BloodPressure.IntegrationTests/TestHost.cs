using BloodPressure.Application.Common.Interfaces;
using BloodPressure.Domain.Models;
using BloodPressure.Functions;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace BloodPressure.IntegrationTests;

public class TestHost
{
    public TestHost()
    {
        var startup = new TestStartup();
        var host = new HostBuilder()
            .ConfigureWebJobs(startup.Configure)
            .ConfigureServices(ReplaceTestOverrides)
            .Build();

        ServiceProvider = host.Services;
        var dbContext = ServiceProvider.GetService<IBloodPressureDbContext>() ?? throw new NullReferenceException();

        var options = ServiceProvider.GetService<IOptions<ApplicationOptions>>() ??
                      throw new NullReferenceException();

        options.Value.IsLocalEnvironment = true;
        dbContext.EnsureDeleted();
        dbContext.Migrate();
    }

    public IServiceProvider ServiceProvider { get; }

    private static void ReplaceTestOverrides(IServiceCollection services)
    {
        services.AddHttpContextAccessor();
    }

    private class TestStartup : Startup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            SetExecutionContextOptions(builder);
            base.Configure(builder);
        }

        private static void SetExecutionContextOptions(IFunctionsHostBuilder builder)
        {
            builder.Services.Configure<ExecutionContextOptions>(o =>
                o.AppDirectory = Directory.GetCurrentDirectory());
        }
    }
}
