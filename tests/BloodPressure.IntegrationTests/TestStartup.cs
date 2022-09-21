using BloodPressure.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BloodPressure.IntegrationTests
{
    public class TestStartup : Startup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            SetExecutionContextOptions(builder);
            base.Configure(builder);

            var services = builder.Services;
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private static void SetExecutionContextOptions(IFunctionsHostBuilder builder)
        {
            builder.Services.Configure<ExecutionContextOptions>(o => o.AppDirectory = Directory.GetCurrentDirectory());
        }
    }
}
