using AutoMapper;
using Moq;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.Exceptions;
using Task_Tracker.BusinessLayer.MapperConfig;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Service;
using Task_Tracker.BusinessLayer.Services;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Tests.TaskServiceTests;

public class TaskServiceNegative
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
    public async Task AddTask_ProjectIdIsNull_ThrowEntityNotFoundException()
    {
        var task = new TaskModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.AddTask(task));
    }

    [Test]
    public async Task DeleteTask_TaskIdIsNull_ThrowEntityNotFoundException()
    {
        var task = new TaskModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.DeleteTask(task.Id));
    }

    [Test]
    public async Task GetTaskById_TaskIdIsNull_ThrowEntityNotFoundException()
    {
        var task = new TaskModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.GetTaskById(task.Id));
    }

    [Test]
    public async Task UpdateTask_TaskIdIsNull_ThrowEntityNotFoundException()
    {
        var task = new TaskModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.UpdateTask(task,task.Id));
    }
}
