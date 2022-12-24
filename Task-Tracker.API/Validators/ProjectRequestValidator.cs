using FluentValidation;
using Task_Tracker.API.Infrastructure;
using Task_Tracker.API.Models.Requests;

namespace Task_Tracker.API.Validators;

public class ProjectRequestValidator : AbstractValidator<ProjectRequest>
{
    public ProjectRequestValidator()
    {

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ApiErrorMessage.NameIsNotRequired);
        RuleFor(p => p.StartDate)
            .NotEmpty();
        RuleFor(p => p.CompletionDate)
        .GreaterThan(DateTime.Now.AddDays(-1)).WithMessage(ApiErrorMessage.StartDateLessCurrent);
        RuleFor(p => p.Priority)
            .InclusiveBetween(1, 10).WithMessage(ApiErrorMessage.PriorityRangeError);
        RuleFor(p=> new {p.StartDate, p.CompletionDate})
            .Must(x=> CustomValidators.ValidStartDateMoreEnd(x.StartDate,x.CompletionDate)).WithMessage(ApiErrorMessage.StartDateCantBeMoreEndDate);
        RuleFor(p => new { p.StartDate, p.CompletionDate })
           .Must(x => CustomValidators.ValidEndDateLessStart(x.StartDate, x.CompletionDate)).WithMessage(ApiErrorMessage.CompletionDateLessStart);



    }

    
}
