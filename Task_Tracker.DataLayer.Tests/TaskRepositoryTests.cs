using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.DataLayer.Tests;

public class TaskRepositoryTests
{
    private DbContextOptions<TaskTrackerContext> _dbContextOptions;

    private TaskRepository _sut;
    private TaskTrackerContext _context;
    private CustomFildRepository _customFildRepository;
    private ProjectRepository _projectRepository;


    public TaskRepositoryTests()
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

        _sut = new TaskRepository(_context);
        _customFildRepository = new CustomFildRepository(_context);
        _projectRepository = new ProjectRepository(_context);
    }

    [Test]
    public async Task AddTask_WhenCorrectData_ThenAddTaskInDbAndReturnId()
    {
         
        var projectId = await _projectRepository.AddProject(new ProjectEntity()
        {
            Id = 2,
            Name = "ProjectTest"
        });

        var task =new TaskEntity()
        {
            Id = 11,
            Name = "Test",
            Discription = "Test",
            Project = new()
            {
                Id = 2
            }
        };


        await _context.SaveChangesAsync();
        var actualId = await _sut.AddTask(task);
        var actualTask =await _sut.GetTaskById(actualId);

        Assert.That(actualId, Is.EqualTo(task.Id));
        Assert.That(task.Name, Is.EqualTo(task.Name));
        Assert.That(task.Discription, Is.EqualTo(task.Discription));
        Assert.That(projectId, Is.EqualTo(task.Project.Id));
    }

    [Test]
    public async Task GetTaskById_WhenCorrectId_ThenTaskReceived()
    {

        var projectId = await _projectRepository.AddProject(new ProjectEntity()
        {
            Id =5,
            Name = "ProjectTest"
        });
        var task = new TaskEntity()
        {
            Id = 5,
            Name = "Task",
            Discription = "Test",
            Project = new()
            {
                Id = 5
            }
        };

        var taskId = await _sut.AddTask(task);

        await _context.SaveChangesAsync();

        var actual = await _sut.GetTaskById(taskId);


        Assert.That(actual.Id, Is.EqualTo(taskId));
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Name, Is.EqualTo(task.Name));
        Assert.That(actual.Priority, Is.EqualTo(task.Priority));
        Assert.That(actual.CurrentStatus, Is.EqualTo(task.CurrentStatus));
        Assert.That(actual.Discription, Is.EqualTo(task.Discription));
    }

    [Test]
    public async Task DeleteTask_WhenCorrectId_ThenDeleted()
    {
        var projectId = await _projectRepository.AddProject(new ProjectEntity()
        {
            Id = 6,
            Name = "ProjectTest"
        });
        var task = new TaskEntity()
        {
            Id = 6,
            Name = "Task",
            Discription = "Test",
            Project = new()
            {
                Id = 6
            }
        };

        var taskId = await _sut.AddTask(task);

        await _context.SaveChangesAsync();

        await _sut.DeleteTask(taskId);

        var actual = await _sut.GetTaskById(taskId);

        Assert.That(actual, Is.EqualTo(null));
    }
}
