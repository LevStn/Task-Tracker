using Task_Tracker.API.Enums;

namespace Task_Tracker.API.Models.Responses;

public class ProjectResponse
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public CurrentStatusProject CurrentStatus { get; set; }
    public int Priority { get; set; }
}
