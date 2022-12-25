using Microsoft.EntityFrameworkCore;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.DataLayer.Tests;

public class CustomFildRepositoryTests
{
    private DbContextOptions<TaskTrackerContext> _dbContextOptions;

    private CustomFildRepository _sut;
    private TaskTrackerContext _context;
    private ProjectRepository _projectRepository;
    private TaskRepository _taskRepository;

    public CustomFildRepositoryTests()
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

        _sut = new CustomFildRepository(_context);
        _projectRepository = new ProjectRepository(_context);
        _taskRepository = new TaskRepository(_context);

    }

    [Test]
    public async Task AddCustomFild_WhenCorrectData_ThenAddCustomFildInDbAndReturnId()
    {

        var projectId = await _projectRepository.AddProject(new ProjectEntity()
        {
            Id = 1,
            Name = "TestProject"
        });

        var taskId = await _taskRepository.AddTask(new TaskEntity()
        {
            Id = 1,
            Name = "TestTask",
            Discription = "Test",
            Project = new()
            {
                Id = 1
            }
        });

        var customFildEntity = new CustomFildEntity()
        {
            Id = 1,
            Name = "Test",
            Meaning = "",
            Task = new()
            {
                Id = 1
            }
        };

        var actualId = await _sut.AddCustomFild(customFildEntity);
        await _context.SaveChangesAsync();

        var expectedCustomFild = await _context.CustomFilds.FirstOrDefaultAsync(t => t.Id == actualId);
        var expectedTask = await _taskRepository.GetTaskById(taskId);
        var customFildsInTask = expectedTask.CustomFilds.ToList();

        Assert.That(actualId, Is.EqualTo(1));
        Assert.That(expectedCustomFild, Is.Not.Null);
        Assert.That(customFildEntity.Name, Is.EqualTo(expectedCustomFild.Name));
        Assert.That(customFildEntity.Meaning, Is.EqualTo(expectedCustomFild.Meaning));
        Assert.That(expectedCustomFild.Task.Id, Is.EqualTo(customFildEntity.Task.Id));
        Assert.That(customFildsInTask[0].Id, Is.EqualTo(customFildEntity.Id));
    }

    [Test]
    public async Task DeleteCustomFild_WhenCorretcId_ThenDeleted()
    {
        var projectId = await _projectRepository.AddProject(new ProjectEntity()
        {
            Id = 1,
            Name = "TestProject"
        });

        var taskId = await _taskRepository.AddTask(new TaskEntity()
        {
            Id = 1,
            Name = "TestTask",
            Discription = "Test",
            Project = new()
            {
                Id = 1
            }
        });

        var customFildEntity = new CustomFildEntity()
        {
            Id = 1,
            Name = "Test",
            Meaning = "",
            Task = new()
            {
                Id = 1
            }
        };

        var customFildId = await _sut.AddCustomFild(customFildEntity);
        
        await _sut.DeleteCustomFild(customFildEntity);

        await _context.SaveChangesAsync();

        var actualTask = await _taskRepository.GetTaskById(taskId);
        var result = actualTask.CustomFilds.ToList();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetCustomFildById_WhenCorrectId_ThenReturnCustomFild()
    {
        var projectId = await _projectRepository.AddProject(new ProjectEntity()
        {
            Id = 1,
            Name = "TestProject"
        });

        var taskId = await _taskRepository.AddTask(new TaskEntity()
        {
            Id = 1,
            Name = "TestTask",
            Discription = "Test",
            Project = new()
            {
                Id = 1
            }
        });

        var customFildEntity = new CustomFildEntity()
        {
            Id = 1,
            Name = "Test",
            Meaning = "Test",
            Task = new()
            {
                Id = 1
            }
        };

        var customFildId = await _sut.AddCustomFild(customFildEntity);

        await _context.SaveChangesAsync();

        var actualCustomFild = await _sut.GetCustomFildById(customFildId);

        Assert.That(actualCustomFild, Is.Not.Null);
        Assert.That(customFildEntity.Name, Is.EqualTo(actualCustomFild.Name));
        Assert.That(customFildEntity.Meaning, Is.EqualTo(actualCustomFild.Meaning));
        Assert.That(customFildEntity.Task.Id, Is.EqualTo(actualCustomFild.Task.Id));
        Assert.That(customFildEntity.Id, Is.EqualTo(actualCustomFild.Id));
    }
}