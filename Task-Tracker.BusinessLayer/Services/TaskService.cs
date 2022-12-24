using AutoMapper;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Services;

public class TaskService : ITaskService
{
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly ICheckerService _checkerService;

    public TaskService(IMapper mapper, ITaskRepository taskRepository, IProjectRepository projectRepository, ICheckerService checkerService)
    {
        _mapper = mapper;
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
        _checkerService= checkerService;
    }

    public async Task<long> AddTask(TaskModel task)
    {
        var project = await _projectRepository.GetProjectById(task.ProjectId);
        _checkerService.CheckIfProjectEmpty(project, task.ProjectId);

        task.CurrentStatus = CurrentStatusTask.ToDo;
        return await _taskRepository.AddTask(_mapper.Map<TaskEntity>(task));
    }

    public async Task DeleteTask(long id)
    {
       var task =await _taskRepository.GetTaskById(id);
       _checkerService.CheckIfTaskEmpty(task, id);
       await _taskRepository.DeleteTask(id);
    }

    public async Task<TaskModel> GetTaskById(long id)
    {
        var task = await _taskRepository.GetTaskById(id);
        _checkerService.CheckIfTaskEmpty(task, id);
        return _mapper.Map<TaskModel>(task);
    }

    public async Task UpdateTask(TaskModel newModel, long id, CurrentStatusTask currentStatusTask)
    {
        var task = await _taskRepository.GetTaskById(id);
        _checkerService.CheckIfTaskEmpty(task, id);

        task.Name = newModel.Name;
        task.CurrentStatus = newModel.CurrentStatus;
        task.Priority = newModel.Priority;
        task.Discription = newModel.Discription;
        task.CurrentStatus = currentStatusTask;

        await _taskRepository.UpdateTask(task);
    }
}
