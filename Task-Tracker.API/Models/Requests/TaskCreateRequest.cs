using Task_Tracker.API.Enums;

namespace Task_Tracker.API.Models.Requests;

public class TaskCreateRequest
{
    public string Name { get; set; }
    public string Discriptions { get; set; }
    public int Priority { get; set; }
}
