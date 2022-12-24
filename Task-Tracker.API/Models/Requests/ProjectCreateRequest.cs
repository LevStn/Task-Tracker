namespace Task_Tracker.API.Models.Requests;

public class ProjectCreateRequest
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate{ get; set; }
    public int Priority { get; set; }

}
