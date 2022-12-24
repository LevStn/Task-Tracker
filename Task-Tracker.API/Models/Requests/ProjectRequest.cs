using System.ComponentModel.DataAnnotations;

namespace Task_Tracker.API.Models.Requests;

public class ProjectRequest
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CompletionDate{ get; set; }
    public int Priority { get; set; }

}
