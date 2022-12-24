using Task_Tracker.BusinessLayer.Exceptions;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Checkers;

public class CheckerService : ICheckerService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public CheckerService(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
        _taskRepository = taskRepository;
    }

    public void CheckIfProjectEmpty(ProjectEntity? project, int id)
    {
        if (project is null)
        {
            throw new EntityNotFoundException($"Project {id} not found");
        }
    }

    public void CheckIfTaskEmpty(TaskEntity? task, long id)
    {
        if (task is null)
        {
            throw new EntityNotFoundException($"Task {id} not found");
        }
    }
    public void CheckIfCustomFildEmpty(CustomFildEntity? customFild, int id)
    {
        if (customFild is null)
        {
            throw new EntityNotFoundException($"Custom fild {id} not found");
        }
    }
}
