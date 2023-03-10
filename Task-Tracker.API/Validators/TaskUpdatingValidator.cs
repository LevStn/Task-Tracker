using FluentValidation;
using Task_Tracker.API.Infrastructure;
using Task_Tracker.API.Models.Requests;

namespace Task_Tracker.API.Validators;

public class TaskUpdatingValidator : AbstractValidator<TaskUpdatingRequest>
{
    public TaskUpdatingValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty().WithMessage(ApiErrorMessage.NameIsNotRequired);
        RuleFor(t => t.Priority)
            .InclusiveBetween(1, 10).WithMessage(ApiErrorMessage.PriorityRangeError);
        RuleFor(t => t.Discription)
            .NotEmpty().WithMessage(ApiErrorMessage.DescriptionRequired);
    }
}

