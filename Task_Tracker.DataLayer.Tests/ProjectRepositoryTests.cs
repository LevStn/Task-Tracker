using Microsoft.EntityFrameworkCore;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.DataLayer.Tests;

public class ProjectRepositoryTests
{
    private DbContextOptions<TaskTrackerContext> _dbContextOptions;

    private ProjectRepository _sut;
    private CustomFildRepository _customFildRepository;
    private TaskTrackerContext _context;
    private TaskRepository _taskRepository;


    public ProjectRepositoryTests()
    {
        _dbContextOptions = new DbContextOptionsBuilder<TaskTrackerContext>()
            .UseInMemoryDatabase(databaseName: "TestDb")
            .Options;
    }

    [SetUp]
    public void Setup()
    {
        if (_context is not null)
            _context.Database.EnsureDeleted();
        _context = new TaskTrackerContext(_dbContextOptions);

        _sut = new ProjectRepository(_context);
        _customFildRepository = new CustomFildRepository(_context);
        _taskRepository = new TaskRepository(_context);
    }

    [Test]
    public async Task AddProject_WhenCorrectData_ThenAddProjectInDbAndReturnId()
    {
        var projectEntity = new ProjectEntity()
        {
            Id =10,
            Name = "TestProject",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(1),
            CurrentStatus = CurrentStatusProject.NotStarted

        };

        var actualId = await _sut.AddProject(projectEntity);

        await _context.SaveChangesAsync();

        Assert.That(actualId, Is.EqualTo(projectEntity.Id));
    }

    [Test]
    public async Task GetProjectById_WhenCorrecId_ThenReturnProject()
    {
        var projectEntity = new ProjectEntity()
        {
            Id = 0,
            Name = "TestProject",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(1),
            CurrentStatus = CurrentStatusProject.NotStarted

        };

        var actualId = await _sut.AddProject(projectEntity);

        var actualProject = await _sut.GetProjectById(actualId);
        await _context.SaveChangesAsync();

        Assert.That(actualId, Is.EqualTo(1));
        Assert.That(actualProject, Is.Not.Null);
        Assert.That(projectEntity.Name, Is.EqualTo(actualProject.Name));
        Assert.That(projectEntity.Priority, Is.EqualTo(actualProject.Priority));
        Assert.That(projectEntity.StartDate, Is.EqualTo(actualProject.StartDate));
        Assert.That(projectEntity.CompletionDate, Is.EqualTo(actualProject.CompletionDate));
        Assert.That(projectEntity.CurrentStatus, Is.EqualTo(actualProject.CurrentStatus));
    }


    [Test]
    public async Task GetProjects_WhenCorrectQuery_ThenReturnAllProject()
    {

        var projectEntityFirst = new ProjectEntity()
        {
            Id = 0,
            Name = "First",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(1),
            CurrentStatus = CurrentStatusProject.NotStarted

        };
        var projectEntitySecond = new ProjectEntity()
        {
            Id = 0,
            Name = "Second",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(2),
            CurrentStatus = CurrentStatusProject.Active

        };
        var first = await _sut.AddProject(projectEntityFirst);
        var second = await _sut.AddProject(projectEntitySecond);

        await _context.SaveChangesAsync();

        var actual = await _sut.GetProjects();

        Assert.AreEqual(actual[0], projectEntityFirst);
        Assert.AreEqual(actual[1], projectEntitySecond);
    }

    [Test]
    public async Task GetTasksByProjectId_WhenCorrectDate_ThenReturnAllTaskInProject()
    {
        var projectId = await _sut.AddProject(new ProjectEntity()
        {
            Id = 1,
            Name = "TestProject"
        });
        var taskIdFirst = await _taskRepository.AddTask(new TaskEntity()
        {
            Id = 1,
            Name = "First",
            Discription = "Test",
            Project = new()
            {
                Id = 1
            }
        });
        var taskIdSecond = await _taskRepository.AddTask(new TaskEntity()
        {
            Id = 2,
            Name = "Second",
            Discription = "Test",
            Project = new()
            {
                Id = 1
            }
            
        });
        var customFildFirstId = await _customFildRepository.AddCustomFild(new CustomFildEntity()
        {
            Id = 1,
            Name = "TestFirst",
            Meaning = "",
            Task = new()
            {
                Id = 1
            }
        });
       

        await _context.SaveChangesAsync();

        var actual = await _sut.GetTasksByProjectId(projectId);

        Assert.That(actual.Count, Is.EqualTo(2));
        Assert.That(actual[0].Id, Is.EqualTo(taskIdFirst));
        Assert.That(actual[1].Id, Is.EqualTo(taskIdSecond));
        Assert.That(actual[0].CustomFilds[0].Id, Is.EqualTo(customFildFirstId));

    }

    [Test]
    public async Task DeleteProject_WhenCorrecId_ThenDeleted()
    {
        var projectId = await _sut.AddProject(new ProjectEntity()
        {
            Id = 1,
            Name = "TestProject"
        });
        await _context.SaveChangesAsync();

        await _sut.DeleteProject(projectId);

        var actual = await _sut.GetProjectById(projectId);

        Assert.That(actual, Is.EqualTo(null));
    }
}
