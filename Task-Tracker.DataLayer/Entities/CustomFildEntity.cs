namespace Task_Tracker.DataLayer.Entities;

public class CustomFildEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Meaning { get; set; }
    public bool IsDeleted { get; set; }
    public TaskEntity Task { get; set; }
}
