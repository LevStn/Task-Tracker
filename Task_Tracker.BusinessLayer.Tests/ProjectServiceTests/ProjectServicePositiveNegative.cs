using AutoMapper;
using Moq;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.Exceptions;
using Task_Tracker.BusinessLayer.MapperConfig;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Service;
using Task_Tracker.BusinessLayer.Services;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Tests.ProjectServiceTests;

public class ProjectServicePositiveNegative
{
    private ProjectService _sut;
    private Mock<ICustomFildRepository> _customFildRepositoryMock;
    private Mock<IProjectRepository> _projectRepositoryMock;
    private Mock<ITaskRepository> _taskRepositoryMock;
    private ICheckerService _checkerService;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigBusinessLayer>()));
        _customFildRepositoryMock = new Mock<ICustomFildRepository>();
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _checkerService = new CheckerService(_taskRepositoryMock.Object, _projectRepositoryMock.Object);
        _sut = new ProjectService(_mapper, _projectRepositoryMock.Object, _checkerService);
    }
    
    [Test]
    public async Task DeleteProject_ProjectIdIsNull_ThrowEntityNotFoundException()
    {
        var project = new ProjectModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.DeleteProject(project.Id));
    }

    [Test]
    public async Task GetProjectById_ProjectIdIsNull_ThrowEntityNotFoundException()
    {
        var project = new ProjectModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.GetProjectById(project.Id));
    }

    [Test]
    public async Task GetTasksByProjectId_ProjectIdIsNull_ThrowEntityNotFoundException()
    {
        var project = new ProjectModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.GetTasksByProjectId(project.Id));
    }

    [Test]
    public async Task UpdateProject_ProjectIdIsNull_ThrowEntityNotFoundException()
    {
        var project = new ProjectModel()
        {
            Id = 2,
            Name = "Test",
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.UpdateProject(project, project.Id));
    }
}
