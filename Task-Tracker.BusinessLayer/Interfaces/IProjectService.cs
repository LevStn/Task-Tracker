using Task_Tracker.BusinessLayer.Models;

namespace Task_Tracker.BusinessLayer.Service
{
    public interface IProjectService
    {
        public Task<int> AddProject(ProjectModel projectModel);
        public Task DeleteProject(int id);
        public Task<ProjectModel> GetProjectById(int id);
        public Task<List<ProjectModel>> GetProjects();
        public Task<List<TaskModel>> GetTasksByProjectId(int id);
        public Task UpdateProject(ProjectModel newModel, int id);
    }
}