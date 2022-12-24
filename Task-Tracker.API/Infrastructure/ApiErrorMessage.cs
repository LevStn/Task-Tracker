namespace Task_Tracker.API.Infrastructure;

public class ApiErrorMessage
{
    public const string NumberLessOrEqualZero = "Number less or equal zero";
    public const string NameIsNotRequired = "Name is not required";
    public const string PriorityRangeError = "Priority must be 1 to 10";
    public const string StartDateLessCurrent = "Start date cannot be less than the current";
    public const string CurrentStatusRangeError = "Current status must be 1 to 3";
    public const string DescriptionRequired = "Description is required";
}
