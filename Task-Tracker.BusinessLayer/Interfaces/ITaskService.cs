﻿using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Enums;

namespace Task_Tracker.BusinessLayer.Services
{
    public interface ITaskService
    {
        Task<long> AddTask(TaskModel task);
        Task DeleteTask(long id);
        Task<TaskModel> GetTaskById(long id);
        Task UpdateTask(TaskModel newModel, long id, CurrentStatusTask currentStatusTask);
    }
}