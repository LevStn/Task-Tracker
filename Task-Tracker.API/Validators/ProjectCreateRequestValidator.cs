using FluentValidation;
using Task_Tracker.API.Infrastructure;
using Task_Tracker.API.Models.Requests;

namespace Task_Tracker.API.Validators;

public class ProjectCreateRequestValidator : AbstractValidator<ProjectCreateRequest>
{
    public ProjectCreateRequestValidator()
    {

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ApiErrorMessage.NameIsNotRequired);
        RuleFor(p => p.StartDate)
            .LessThan(DateTime.Now).WithMessage(ApiErrorMessage.StartDateLessCurrent);
        RuleFor(p => p.CompletionDate)
            .NotEmpty();
        RuleFor(p => p.Priority)
            .InclusiveBetween(1, 10).WithMessage(ApiErrorMessage.PriorityRangeError);

    }
}
