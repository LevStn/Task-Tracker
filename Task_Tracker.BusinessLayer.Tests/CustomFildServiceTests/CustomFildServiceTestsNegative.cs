using AutoMapper;
using Moq;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.Exceptions;
using Task_Tracker.BusinessLayer.MapperConfig;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Services;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Tests.CustomFildServiceTests;

public class CustomFildServiceTestsNegative
{
    private CustomFildService _sut;
    private Mock<ICustomFildRepository> _customFildRepositoryMock;
    private ICheckerService _checkerService;
    private Mock<ITaskRepository> _taskRepositoryMock;
    private Mock<IProjectRepository> _projectRepositoryMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigBusinessLayer>()));
        _customFildRepositoryMock = new Mock<ICustomFildRepository>();
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _checkerService = new CheckerService(_taskRepositoryMock.Object, _projectRepositoryMock.Object);
        _sut = new CustomFildService(_mapper, _customFildRepositoryMock.Object, _checkerService, _taskRepositoryMock.Object);
    }

    [Test]
    public async Task AddCustomFild_TaskIsNull_ThrowEntityNotFoundException()
    {
        var customFild = new CustomFildModel()
        {
            Id = 2,
            Name = "Test",
            TaskId = 1,
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.AddCustomFild(_mapper.Map<CustomFildModel>(customFild)));
    }

    [Test]
    public async Task DeleteCustomFild_CustomFildIdIsNull_ThrowEntityNotFoundException()
    {
        var customFild = new CustomFildModel()
        {
            Id = 2,
            Name = "Test",
            TaskId = 1,
        };

        Assert.ThrowsAsync<EntityNotFoundException>(() => _sut.DeleteCustomFild(1));
    }
}
