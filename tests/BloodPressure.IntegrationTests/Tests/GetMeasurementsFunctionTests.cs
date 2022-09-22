using BloodPressure.Application.Common.Dtos;
using BloodPressure.Application.Common.Interfaces;
using BloodPressure.Domain.Entities;
using BloodPressure.Functions.Functions;
using BloodPressure.IntegrationTests.Utils;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BloodPressure.IntegrationTests.Tests
{
    [Collection(IntegrationTestsCollection.Name)]
    public class GetMeasurementsFunctionTests
    {
        private readonly GetMeasurementsFunction _function;
        private readonly IBloodPressureDbContext _dbContext;

        public GetMeasurementsFunctionTests(TestHost testHost)
        {
            _function = new GetMeasurementsFunction(testHost.ServiceProvider.GetService<IBloodPressureService>() ??
                                                    throw new InvalidOperationException(),
                testHost.ServiceProvider.GetService<ILogger<GetMeasurementsFunction>>() ??
                throw new InvalidOperationException());

            _dbContext = testHost.ServiceProvider.GetService<IBloodPressureDbContext>() ??
                         throw new InvalidOperationException();
        }

        [Fact]
        public async Task ShouldGetMeasurements()
        {
            // TODO Add helper classes to insert and create new entities for testing
            var measurement = new Measurement(1, 100, 120, new DateTime(2020, 1, 1), "test");
            await _dbContext.Measurement.AddAsync(measurement);
            await _dbContext.SaveChangesAsync(CancellationToken.None);

            var getMeasurements = new GetMeasurementsDto(null, DateTime.Now, "test");
            var res = await _function.Run(getMeasurements, CancellationToken.None);

            res.Measurements.Count.Should().Be(1);
            res.Measurements[0].Diastolic.Should().Be(measurement.Diastolic);
            res.Measurements[0].Systolic.Should().Be(measurement.Systolic);
            res.Measurements[0].Pulse.Should().Be(measurement.Pulse);
            res.Measurements[0].MeasuringDate.Should().Be(measurement.MeasuringDate);
            res.To.Should().Be(getMeasurements.To);
            res.From.Should().Be(getMeasurements.From);
            res.UserId.Should().Be(getMeasurements.UserId);
        }
    }
}
