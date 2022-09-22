using BloodPressure.Application.Common.Dtos;
using BloodPressure.Application.Common.Extensions;
using FluentValidation.TestHelper;
using Xunit;
using static System.DateTime;

namespace BloodPressure.UnitTests.Tests.ValidatorTests;

public class CreateMeasurementDtoValidatorTests
{
    public static IEnumerable<object[]> Dates =>
        new List<object[]>
        {
            new object[] {Today.EndOfDay().AddMinutes(1)}
        };

    [Theory]
    [InlineData(-1)]
    [InlineData(501)]
    public void PulseCannotBeNegativeOrOver500(int? pulse)
    {
        // Arrange
        var request = new CreateMeasurementDto(pulse, 50, 50, Now);
        var validator = new CreateMeasurementDtoValidator();

        // Act
        var result =
            validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Pulse);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(501)]
    public void SystolicCannotBeNegativeOrOver500(int systolic)
    {
        // Arrange
        var request = new CreateMeasurementDto(10, systolic, 50, Now);
        var validator = new CreateMeasurementDtoValidator();

        // Act
        var result =
            validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Systolic);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(501)]
    public void DiastolicCannotBeNegativeOrOver500(int diastolic)
    {
        // Arrange
        var request = new CreateMeasurementDto(50, 50, diastolic, Now);
        var validator = new CreateMeasurementDtoValidator();

        // Act
        var result =
            validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Diastolic);
    }

    [Theory]
    [MemberData(nameof(Dates))]
    public void MeasuringDateMustBeTodayOrBefore(DateTime dateTime)
    {
        // Arrange
        var request = new CreateMeasurementDto(50, 50, 50, dateTime);
        var validator = new CreateMeasurementDtoValidator();

        // Act
        var result =
            validator.TestValidate(request);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.MeasuringDate);
    }
}
