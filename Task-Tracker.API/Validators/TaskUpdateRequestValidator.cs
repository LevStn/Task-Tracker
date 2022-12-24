using FluentValidation;
using Task_Tracker.API.Infrastructure;
using Task_Tracker.API.Models.Requests;

namespace Task_Tracker.API.Validators;

public class TaskUpdateRequestValidator : AbstractValidator<TaskUpdateRequest>
{
    public TaskUpdateRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ApiErrorMessage.NameIsNotRequired);
        RuleFor(p => p.Priority)
            .InclusiveBetween(1, 10).WithMessage(ApiErrorMessage.PriorityRangeError);
        RuleFor(t => t.CurrentStatus)
            .IsInEnum().WithMessage(ApiErrorMessage.CurrentStatusRangeError);
        RuleFor(t => t.Discription)
            .NotEmpty().WithMessage(ApiErrorMessage.DescriptionRequired);
    }
}
