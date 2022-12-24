namespace Task_Tracker.API.Validators;

public static class CustomValidators
{
    public static bool ValidStartDateMoreEnd(DateTime startDate, DateTime endDate)
    {
        if (startDate > endDate)
        {
            return false;
        }
        return true;
    }

    public static bool ValidEndDateLessStart(DateTime startDate, DateTime endDate)
    {
        if (startDate <= endDate)
        {
            return true;
        }
        return false;
    }
}
