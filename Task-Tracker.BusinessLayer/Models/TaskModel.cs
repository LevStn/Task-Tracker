using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.BusinessLayer.Models;

public class TaskModel
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CurrentStatusTask CurrentStatus { get; set; }
    public string Discription { get; set; }
    public int Priority { get; set; }
    public int ProjectId { get; set; }
    public List<CustomFildModel> CustomFildModels { get; set; }
}
