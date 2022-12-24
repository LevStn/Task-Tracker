using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.API.Models.Requests;

public class TaskUpdateRequest
{
    public string Name { get; set; }
    public CurrentStatusTask CurrentStatus { get; set; }
    public string? Discription { get; set; }
    public int Priority { get; set; }
}
