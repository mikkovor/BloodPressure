using BloodPressure.Application.Common.Constants;
using BloodPressure.Application.Common.Dtos;
using BloodPressure.Application.Common.Interfaces;
using BloodPressure.Functions.Functions;
using BloodPressure.IntegrationTests.Utils;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace BloodPressure.IntegrationTests.Tests;

[Collection(IntegrationTestsCollection.Name)]
public class CreateMeasurementTests
{
    private readonly CreateMeasurementFunction _function;

    public CreateMeasurementTests(TestHost testHost)
    {
        _function = new CreateMeasurementFunction(testHost.ServiceProvider.GetService<IBloodPressureService>() ??
                                                  throw new InvalidOperationException());
    }

    [Fact]
    public async Task ShouldCreateMeasurementDto()
    {
        var dateTime = new DateTime(2022, 9, 21);
        var createMeasurement = new CreateMeasurementDto(60, 120, 60, dateTime);
        var res = await _function.Run(createMeasurement, CancellationToken.None);

        var newMeasurement = res.GetObjectResultContent();
        newMeasurement.Should().NotBeNull();
        newMeasurement?.Diastolic.Should().Be(createMeasurement.Diastolic);
        newMeasurement?.Pulse.Should().Be(createMeasurement.Pulse);
        newMeasurement?.Systolic.Should().Be(createMeasurement.Systolic);
        newMeasurement?.Diastolic.Should().Be(createMeasurement.Diastolic);
        newMeasurement?.MeasuringDate.Should().Be(dateTime);
        newMeasurement?.UserId.Should().Be(LocalConstants.LocalUserId);
    }
}