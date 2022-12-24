using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.API.Models.Responses;

public class TaskResponse
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CurrentStatusTask CurrentStatus { get; set; }
    public string? Discriptions { get; set; }
    public int Priority { get; set; }
    public int ProjectId { get; set; }
    public List<CustomFildResponse> CustomFilds { get; set; }
}
