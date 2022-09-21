using System.IO;
using BloodPressure.Infrastructure;
using BloodPressure.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace BloodPressure.Functions;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BloodPressureDbContext>
{
    BloodPressureDbContext IDesignTimeDbContextFactory<BloodPressureDbContext>.CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("local.settings.json", false, true)
            .AddEnvironmentVariables()
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var builder = new DbContextOptionsBuilder<BloodPressureDbContext>();
        builder.UseSqlServer(connectionString);
        return new BloodPressureDbContext(builder.Options, new DateTimeService());
    }
}