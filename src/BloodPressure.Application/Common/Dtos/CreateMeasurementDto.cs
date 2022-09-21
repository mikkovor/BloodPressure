using BloodPressure.Application.Common.Extensions;
using BloodPressure.Domain.Entities;
using FluentValidation;

namespace BloodPressure.Application.Common.Dtos;

public record CreateMeasurementDto(int? Pulse, int Systolic, int Diastolic, DateTime MeasuringDate)
{
    public Measurement ToEntity(string userId)
    {
        return new Measurement(Pulse, Systolic, Diastolic, MeasuringDate, userId);
    }
}

public class CreateMeasurementDtoValidator : AbstractValidator<CreateMeasurementDto>
{
    public CreateMeasurementDtoValidator()
    {
        RuleFor(x => x.Systolic).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(500);
        RuleFor(x => x.Diastolic).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(500);
        RuleFor(x => x.Pulse).NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(500);
        RuleFor(x => x.MeasuringDate).NotEmpty().LessThanOrEqualTo(DateTime.Now.EndOfDay());
    }
}