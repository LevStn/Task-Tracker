namespace Task_Tracker.API.Models.Requests;

public class CustomFildRequest
{
    public long TaskId { get; set; }
    public string Name { get; set; }
    public string? Meaning { get; set; }
}
