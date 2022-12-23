using Task_Tracker.API.Enums;

namespace Task_Tracker.API.Models.Requests;

public class ProjectUpdateRequest
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public CurrentStatusProject CurrentStatus { get; set; }
    public int Priority { get; set; }
}
