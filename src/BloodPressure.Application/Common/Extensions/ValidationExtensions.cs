using FluentValidation;
using FluentValidation.Results;

namespace BloodPressure.Application.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static bool IsValid<TModel, TValidator>(this TModel model, out ValidationResult validationResult)
            where TValidator : AbstractValidator<TModel>, new()
        {
            var validator = new TValidator();
            validationResult = validator.Validate(model);
            return validationResult.IsValid;
        }
    }
}
