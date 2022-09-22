using BloodPressure.Application.Common.Dtos;
using FluentValidation.TestHelper;
using Xunit;

namespace BloodPressure.UnitTests.Tests.ValidatorTests;

public class GetMeasurementsDtoValidatorTests
{
    [Fact]
    public void ShouldNotAllowFromBeGreaterThanToAndToBeLessThanFrom()
    {
        var request = new GetMeasurementsDto(new DateTime(2022, 10, 12), new DateTime(2022, 10, 11));
        var validator = new GetMeasurementsDtoValidator();

        var result = validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.From);
        result.ShouldHaveValidationErrorFor(x => x.To);
    }

    [Fact]
    public void ShouldHaveFromOrToDefined()
    {
        var request = new GetMeasurementsDto(null, null);
        var validator = new GetMeasurementsDtoValidator();

        var result = validator.TestValidate(request);

        result.ShouldHaveValidationErrorFor(x => x.From);
        result.ShouldHaveValidationErrorFor(x => x.To);
    }

    [Fact]
    public void ShouldAllowFromWithoutTo()
    {
        var request = new GetMeasurementsDto(new DateTime(2022, 10, 12), null);
        var validator = new GetMeasurementsDtoValidator();

        var result = validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void ShouldAllowToWithoutFrom()
    {
        var request = new GetMeasurementsDto(null, new DateTime(2022, 10, 12));
        var validator = new GetMeasurementsDtoValidator();

        var result = validator.TestValidate(request);

        result.ShouldNotHaveAnyValidationErrors();
    }
}