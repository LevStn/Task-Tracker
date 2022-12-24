using AutoMapper;
using Moq;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.MapperConfig;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Service;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Tests.ProjectServiceTests;

public class ProjectServicePositive
{
    private ProjectService _sut;
    private Mock<ICheckerService> _checkerServiceMock;
    private Mock<IProjectRepository> _projectRepositoryMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigBusinessLayer>()));
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _checkerServiceMock = new Mock<ICheckerService>();  
        _sut = new ProjectService(_mapper, _projectRepositoryMock.Object, _checkerServiceMock.Object);
    }

    [Test]
    public async Task AddProject_ValidRequestPassed_AddProjectIdReturned()
    {
        var projectModel = new ProjectModel()
        {
            Id = 5,
            Name = "First",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(1),
            CurrentStatus = CurrentStatusProject.NotStarted
        };

        _projectRepositoryMock.Setup(p => p.AddProject(It.Is<ProjectEntity>(p => p.Id == projectModel.Id)))
            .ReturnsAsync(projectModel.Id);

        var actual = await _sut.AddProject(projectModel);

        Assert.That(actual, Is.EqualTo(projectModel.Id));
        _projectRepositoryMock.Verify(p => p.AddProject(It.Is<ProjectEntity>(p =>
        p.Id == projectModel.Id &&
        p.Name == projectModel.Name &&
        p.Priority == projectModel.Priority &&
        p.StartDate == projectModel.StartDate &&
        p.CompletionDate == projectModel.CompletionDate &&
        p.CurrentStatus == projectModel.CurrentStatus)));
    }   

    [Test]
    public async Task GetProjectById_ValidRequestPassed_ProjectReturned()
    {
        var projectModel = new ProjectEntity()
        {
            Id = 5,
            Name = "First",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(1),
            CurrentStatus = CurrentStatusProject.NotStarted
        };

        _projectRepositoryMock.Setup(p => p.GetProjectById(projectModel.Id))
            .ReturnsAsync(projectModel);

        var actual = await _sut.GetProjectById(projectModel.Id);

        Assert.That(actual.Id, Is.EqualTo(projectModel.Id));
        Assert.That(actual.Name, Is.EqualTo(projectModel.Name));
        Assert.That(actual.Priority, Is.EqualTo(projectModel.Priority));
        Assert.That(actual.StartDate, Is.EqualTo(projectModel.StartDate));
        Assert.That(actual.CompletionDate, Is.EqualTo(projectModel.CompletionDate));
        Assert.That(actual.CurrentStatus, Is.EqualTo(projectModel.CurrentStatus));
    }

    [Test]
    public async Task GetProjects_ValidQuery_ProjectsReturned()
    {
        var listProject = new List<ProjectEntity>()
        {
            new()
            {
                Id = 5,
                Name = "First",
                Priority = 1,
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now.AddDays(1),
                CurrentStatus = CurrentStatusProject.NotStarted
            },
            new()
            {
                Id = 6,
                Name = "First",
                Priority = 1,
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now.AddDays(1),
                CurrentStatus = CurrentStatusProject.NotStarted
            },
             new()
            {
                Id = 7,
                Name = "First",
                Priority = 1,
                StartDate = DateTime.Now,
                CompletionDate = DateTime.Now.AddDays(1),
                CurrentStatus = CurrentStatusProject.NotStarted
            }
        };
       

        _projectRepositoryMock.Setup(p => p.GetProjects())
            .ReturnsAsync(listProject);

        var actual = await _sut.GetProjects();

        Assert.That(actual.Count, Is.EqualTo(listProject.Count));
        
    }

    [Test]
    public async Task GetTasksByProjectId_ValidQuery_TasksReturned()
    {
        var project = new ProjectEntity()
        {
            Id = 9,
            Name = "First",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(1),
            CurrentStatus = CurrentStatusProject.NotStarted,
            Task = new()
            {
                new()
                {
                    Id = 1,
                    Name ="First"
                },
                new()
                {
                    Id =2,
                    Name="Second"
                }
            }
        };

        _projectRepositoryMock.Setup(p => p.GetTasksByProjectId(project.Id))
           .ReturnsAsync(project.Task);

        var actual = await _sut.GetTasksByProjectId(project.Id);

        Assert.That(actual.Count, Is.EqualTo(project.Task.Count));
    }

    [Test]
    public async Task UpdateProject_ValidRequestPassed_ChangesProperties()
    {
        var project = new ProjectEntity()
        {
            Id = 9,
            Name = "First",
            Priority = 1,
            StartDate = DateTime.Now,
            CompletionDate = DateTime.Now.AddDays(1),
            CurrentStatus = CurrentStatusProject.NotStarted,
            
        };

        var newProjectProperties = new ProjectModel()
        {
            Name = "Second",
            Priority = 10,
            StartDate = DateTime.Now.AddDays(1),
            CompletionDate = DateTime.Now,
            CurrentStatus = CurrentStatusProject.Active,
        };

        _projectRepositoryMock.Setup(p => p.GetProjectById(project.Id))
           .ReturnsAsync(project);

        await _sut.UpdateProject(newProjectProperties, project.Id);

        var actual = await _sut.GetProjectById(project.Id);

        Assert.That(actual.Name, Is.EqualTo(newProjectProperties.Name));
        Assert.That(actual.Priority, Is.EqualTo(newProjectProperties.Priority));
        Assert.That(actual.StartDate, Is.EqualTo(newProjectProperties.StartDate));
        Assert.That(actual.CompletionDate, Is.EqualTo(newProjectProperties.CompletionDate));
        Assert.That(actual.CurrentStatus, Is.EqualTo(newProjectProperties.CurrentStatus));
    }


    [TestCase(TypeOFSorting.AscendingStartDate)]
    [TestCase(TypeOFSorting.DescendingStartDate)]
    [TestCase(TypeOFSorting.AscendingCompletionDate)]
    [TestCase(TypeOFSorting.DescendingCompletionDate)]
    [TestCase(TypeOFSorting.AscendingPriority)]
    [TestCase(TypeOFSorting.DescendingPriority)]
    [Test]
    public async Task SortingProjectByParameters_ValidQuery_ProjectSortingReturned(TypeOFSorting typeOFSorting)
    {
        var expectedAscendingStartDate = new List<int>() { 9, 11, 10, 12 };
        var expectedDescendingStartDate = expectedAscendingStartDate.AsEnumerable().Reverse().ToList();
        var expectedAscendingCompletionDate = new List<int>() {11, 9, 12, 10 };
        var expectedDescendingCompletionDate = expectedAscendingCompletionDate.AsEnumerable().Reverse().ToList();
        var expectedAscendingPriority = new List<int>() { 9, 12, 10, 11 };
        var expectedDescendingPriority = expectedAscendingPriority.AsEnumerable().Reverse().ToList();

        var projects = new List<ProjectEntity>()
        {
            new()
            {

                Id = 9,
                Name = "First",
                Priority = 1,
                StartDate = DateTime.Now.AddDays(1),
                CompletionDate = DateTime.Now.AddDays(3),
                CurrentStatus = CurrentStatusProject.NotStarted,

            },
            new()
            {

                Id = 10,
                Name = "Second",
                Priority = 5,
                StartDate = DateTime.Now.AddDays(5),
                CompletionDate = DateTime.Now.AddDays(10),
                CurrentStatus = CurrentStatusProject.NotStarted,

            },
            new()
            {

                Id = 11,
                Name = "Fourth",
                Priority = 10,
                StartDate = DateTime.Now.AddDays(2),
                CompletionDate = DateTime.Now.AddDays(2),
                CurrentStatus = CurrentStatusProject.NotStarted,

            },
            new()
            {

                Id = 12,
                Name = "Fifth",
                Priority = 2,
                StartDate = DateTime.Now.AddDays(7),
                CompletionDate = DateTime.Now.AddDays(5),
                CurrentStatus = CurrentStatusProject.NotStarted,

            },
        };
       

       _projectRepositoryMock.Setup(p => p.GetProjects())
           .ReturnsAsync(projects);

        var actual = await _sut.SortingProjectByParameters(typeOFSorting);
        var actualSortId = new List<int> { actual[0].Id, actual[1].Id, actual[2].Id, actual[3].Id };

        switch (typeOFSorting)
        {
            case TypeOFSorting.AscendingStartDate:
                Assert.AreEqual(actualSortId, expectedAscendingStartDate);
                break;
            case TypeOFSorting.DescendingStartDate:
                Assert.AreEqual(actualSortId, expectedDescendingStartDate);
                break;
            case TypeOFSorting.AscendingCompletionDate:
                Assert.AreEqual(actualSortId, expectedAscendingCompletionDate);
                break;
            case TypeOFSorting.DescendingCompletionDate:
                Assert.AreEqual(actualSortId, expectedDescendingCompletionDate);
                break;
            case TypeOFSorting.AscendingPriority:
                Assert.AreEqual(actualSortId, expectedAscendingPriority);
                break;
            case TypeOFSorting.DescendingPriority:
                Assert.AreEqual(actualSortId, expectedDescendingPriority);
                break;
        }
        
      
    }
}
