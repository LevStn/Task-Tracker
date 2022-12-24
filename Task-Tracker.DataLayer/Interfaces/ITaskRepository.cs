using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer.Repositories
{
    public interface ITaskRepository
    {
        public Task<long> AddTask(TaskEntity task);
        public Task DeleteTask(long id);
        public Task<TaskEntity?> GetTaskById(long id);
        public Task UpdateTask(TaskEntity newModel);
    }
}