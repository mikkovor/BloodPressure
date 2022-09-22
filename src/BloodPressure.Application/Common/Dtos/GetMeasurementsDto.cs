using FluentValidation;

namespace BloodPressure.Application.Common.Dtos;

public record GetMeasurementsDto(DateTime? From, DateTime? To);

public class GetMeasurementsDtoValidator : AbstractValidator<GetMeasurementsDto>
{
    public GetMeasurementsDtoValidator()
    {
        When(x => x.From == null, () => { RuleFor(x => x.To).NotEmpty(); });
        When(x => x.To == null, () => { RuleFor(x => x.From).NotEmpty(); });
        When(x => x.From != null && x.To != null, () =>
        {
            RuleFor(x => x.From).LessThan(x => x.To);
            RuleFor(x => x.To).GreaterThan(x => x.From);
        });
    }
}