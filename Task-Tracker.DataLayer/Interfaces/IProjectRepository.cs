using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer.Repositories
{
    public interface IProjectRepository
    {
        public Task<int> AddProject(ProjectEntity project);
        public Task DeleteProject(int id);
        public Task<ProjectEntity> GetProjectById(int id);
        public Task<List<ProjectEntity>> GetProjects();
        public Task<List<TaskEntity>> GetTasksByProjectId(int id);
        public Task UpdateProject(ProjectEntity newModel);
    }
}