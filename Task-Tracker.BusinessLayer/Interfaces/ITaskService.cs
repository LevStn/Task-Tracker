using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.BusinessLayer.Services;

public interface ITaskService
{
    public Task<long> AddTask(TaskModel task);
    public Task DeleteTask(long id);
    public Task<TaskModel> GetTaskById(long id);
    public Task UpdateTask(TaskModel newModel, long id, CurrentStatusTask currentStatusTask);
}