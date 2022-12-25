using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.BusinessLayer.Service;

public interface IProjectService
{
    public Task<int> AddProject(ProjectModel projectModel);
    public Task DeleteProject(int id);
    public Task<ProjectModel> GetProjectById(int id);
    public Task<List<ProjectModel>> GetProjects();
    public Task<List<TaskModel>> GetTasksByProjectId(int id);
    public Task UpdateProject(ProjectModel newModel, int id, CurrentStatusProject currentStatus);
    public Task<List<ProjectModel>> SortingProjectByParameters(TypeOFSorting typeOFSorting);
}