using FluentValidation;
using Task_Tracker.API.Infrastructure;
using Task_Tracker.API.Models.Requests;

namespace Task_Tracker.API.Validators;

public class ProjectUpdateRequestValidator : AbstractValidator<ProjectUpdateRequest>
{
    public ProjectUpdateRequestValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ApiErrorMessage.NameIsNotRequired);
        RuleFor(p => p.CompletionDate)
            .NotEmpty();
        RuleFor(p => p.Priority)
            .InclusiveBetween(1, 10).WithMessage(ApiErrorMessage.PriorityRangeError);
        RuleFor(p => p.CurrentStatus)
            .IsInEnum().WithMessage(ApiErrorMessage.CurrentStatusRangeError);
    }
}

