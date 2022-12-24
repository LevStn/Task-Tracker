using AutoMapper;
using Moq;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.MapperConfig;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Services;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Tests.TaskServiceTests;

public class TaskServicePositive
{
    private TaskService _sut;
    private Mock<ITaskRepository> _taskRepositoryMock;
    private Mock<IProjectRepository> _projectRepositoryMock;
    private ICheckerService _checkerService;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigBusinessLayer>()));
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _checkerService = new CheckerService(_taskRepositoryMock.Object, _projectRepositoryMock.Object);
        _sut = new TaskService(_mapper, _taskRepositoryMock.Object, _projectRepositoryMock.Object, _checkerService);
    }

    [Test]
    public async Task AddTask_ValidRequestPassed_AddTaskIdReturned()
    {
        var task = new TaskModel()
        {
           Id = 5,
           Name = "Task",
           ProjectId =1,
           Discription =""
           
        };
        var project = new ProjectEntity()
        {
            Id = 1,
            Name = "Project"
        };

        _taskRepositoryMock.Setup(p => p.AddTask(It.Is<TaskEntity>(p => p.Id == task.Id)))
            .ReturnsAsync(task.Id);
        _projectRepositoryMock.Setup(p => p.GetProjectById(task.ProjectId))
          .ReturnsAsync(project);

        var actual = await _sut.AddTask(task);

        Assert.That(actual, Is.EqualTo(task.Id));
        _taskRepositoryMock.Verify(p => p.AddTask(It.Is<TaskEntity>(p =>
        p.Id == task.Id &&
        p.Name == task.Name &&
        p.Priority == task.Priority &&
        p.Discription == task.Discription &&
        p.CurrentStatus == task.CurrentStatus &&
        p.Priority == task.Priority)));
    }

    [Test]
    public async Task GetTaskById_ValidRequestPassed_TaskReturned()
    {
        var taskModel = new TaskEntity()
        {
            Id = 5,
            Name = "First",
            Priority = 1,
            Discription = "Test",
            CurrentStatus = CurrentStatusTask.ToDo
        };

        _taskRepositoryMock.Setup(p => p.GetTaskById(taskModel.Id))
            .ReturnsAsync(taskModel);

        var actual = await _sut.GetTaskById(taskModel.Id);

        Assert.That(actual.Id, Is.EqualTo(taskModel.Id));
        Assert.That(actual.Name, Is.EqualTo(taskModel.Name));
        Assert.That(actual.Priority, Is.EqualTo(taskModel.Priority));
        Assert.That(actual.Discription, Is.EqualTo(taskModel.Discription));
        Assert.That(actual.CurrentStatus, Is.EqualTo(taskModel.CurrentStatus));
        Assert.That(actual.Priority, Is.EqualTo(taskModel.Priority));
    }

    [Test]
    public async Task UpdateProject_ValidRequestPassed_ChangesProperties()
    {
        var task = new TaskEntity()
        {
            Id = 5,
            Name = "First",
            Priority = 1,
            Discription = "Test",
            CurrentStatus = CurrentStatusTask.ToDo

        };

        var newTaskProperties = new TaskModel()
        {
            Name = "Second",
            Priority = 10,
            Discription="newTest",
            CurrentStatus = CurrentStatusTask.ToDo,
        };

        _taskRepositoryMock.Setup(p => p.GetTaskById(task.Id))
           .ReturnsAsync(task);

        await _sut.UpdateTask(newTaskProperties, task.Id);

        var actual = await _sut.GetTaskById(task.Id);

        Assert.That(actual.Name, Is.EqualTo(newTaskProperties.Name));
        Assert.That(actual.Priority, Is.EqualTo(newTaskProperties.Priority));
        Assert.That(actual.Discription, Is.EqualTo(newTaskProperties.Discription));
        Assert.That(actual.CurrentStatus, Is.EqualTo(newTaskProperties.CurrentStatus));
    }
}
