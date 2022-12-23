namespace Task_Tracker.BusinessLayer.Models;

public class TaskModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    //public CurrentStatusTask CurrentStatus { get; set; }
    public string Discriptions { get; set; }
    public int Priority { get; set; }
}
