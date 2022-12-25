using AutoMapper;
using Moq;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.MapperConfig;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Services;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Tests.CustomFildServiceTests;

public class CustomFildServiceTestsPositive
{
    private CustomFildService _sut;
    private Mock<ICustomFildRepository> _customFildRepositoryMock;
    private Mock<ICheckerService> _checkerServiceMock;
    private Mock< ITaskRepository> _taskRepositoryMock;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mapper= new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<MapperConfigBusinessLayer>()));
        _customFildRepositoryMock = new Mock<ICustomFildRepository>();
        _checkerServiceMock = new Mock<ICheckerService>();
        _taskRepositoryMock = new Mock<ITaskRepository>();
        _sut = new CustomFildService(_mapper, _customFildRepositoryMock.Object, _checkerServiceMock.Object, _taskRepositoryMock.Object);
    }

    [Test]
    public async Task AddCustomFild_ValidRequestPassed_AddCustomFildIdReturned()
    {
        var customFild = new CustomFildModel()
        {
            Id = 2,
            Name = "Test",
            TaskId = 1,
        };

        _customFildRepositoryMock.Setup(c => c.AddCustomFild(It.Is<CustomFildEntity>(c => c.Name == customFild.Name)))
            .ReturnsAsync(customFild.Id);

        var actual = await _sut.AddCustomFild(customFild);

        Assert.That(actual, Is.EqualTo(customFild.Id));
        _customFildRepositoryMock.Verify(p => p.AddCustomFild(It.Is<CustomFildEntity>(p =>
          p.Id == customFild.Id &&
          p.Name == customFild.Name &&
          p.Task.Id == customFild.TaskId)));
    }
}
