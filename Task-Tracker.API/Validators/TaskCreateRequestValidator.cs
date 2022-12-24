using FluentValidation;
using Task_Tracker.API.Infrastructure;
using Task_Tracker.API.Models.Requests;

namespace Task_Tracker.API.Validators;

public class TaskCreateRequestValidator : AbstractValidator<TaskCreateRequest>
{
    public TaskCreateRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ApiErrorMessage.NameIsNotRequired);
        RuleFor(p => p.Priority)
            .InclusiveBetween(1, 10).WithMessage(ApiErrorMessage.PriorityRangeError); ;
        RuleFor(p => p.ProjectId)
            .GreaterThan(0).WithMessage(ApiErrorMessage.NumberLessOrEqualZero);
    }
}


