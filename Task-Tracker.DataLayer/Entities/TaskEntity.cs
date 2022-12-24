using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.DataLayer.Entities;

public class TaskEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public CurrentStatusTask CurrentStatus { get; set; }
    public string Discription { get; set; }
    public int Priority { get; set; }
    public bool IsDeleted { get; set; }
    public  ProjectEntity Project { get; set; }
    public virtual ICollection<CustomFildEntity> CustomFilds { get; set; }
}

