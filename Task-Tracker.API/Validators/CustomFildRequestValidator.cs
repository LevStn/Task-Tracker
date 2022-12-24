using FluentValidation;
using Task_Tracker.API.Infrastructure;
using Task_Tracker.API.Models.Requests;

namespace Task_Tracker.API.FluentValidators;

public class CustomFildRequestValidator : AbstractValidator<CustomFildRequest>
{
    public CustomFildRequestValidator()
    {

        RuleFor(t => t.TaskId)
            .GreaterThan(0).WithMessage(ApiErrorMessage.NumberLessOrEqualZero);
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage(ApiErrorMessage.NameIsNotRequired);
    }
}


