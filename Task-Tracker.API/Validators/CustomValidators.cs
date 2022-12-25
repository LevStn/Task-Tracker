namespace Task_Tracker.API.Validators;

public static class CustomValidators
{
    public static bool ValidationStartDateMoreEnd(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return false;
        }
        return true;
    }

    public static bool ValidationEndDateLessStart(DateTime startDate, DateTime endDate)
    {
        if (startDate <= endDate)
        {
            return true;
        }
        return false;
    }

    public static bool ValidationDate(string value)
    {
        DateTime date;
        return DateTime.TryParse(value, out date);
    }
}
