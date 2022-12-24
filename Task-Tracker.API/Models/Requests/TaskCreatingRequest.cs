namespace Task_Tracker.API.Models.Requests;

public class TaskCreatingRequest
{
    public string Name { get; set; }
    public string Discription { get; set; }
    public int Priority { get; set; }
    public int ProjectId { get; set; }
}
