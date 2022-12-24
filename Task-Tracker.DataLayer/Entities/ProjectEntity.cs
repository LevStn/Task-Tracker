using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.DataLayer.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate { get; set; }
    public CurrentStatusProject CurrentStatus { get; set; }
    public int Priority { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<TaskEntity> Task { get; set; }
}
