using AutoMapper;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Services;

public class CustomFildService : ICustomFildService
{
    private readonly IMapper _mapper;
    private readonly ICustomFildRepository _customFildRepository;
    private readonly ICheckerService _checkerService;
    private readonly ITaskRepository _taskRepository;

    public CustomFildService(IMapper mapper, ICustomFildRepository customFildRepository,
        ICheckerService checkerService, ITaskRepository taskRepository)
    {
        _mapper = mapper;
        _customFildRepository = customFildRepository;
        _checkerService = checkerService;
        _taskRepository = taskRepository;
    }
    public async Task<int> AddCustomFild(CustomFildModel customFild)
    {
        var task = await _taskRepository.GetTaskById(customFild.TaskId);
        _checkerService.CheckIfTaskEmpty(task, customFild.Id);
        return await _customFildRepository.AddCustomFild(_mapper.Map<CustomFildEntity>(customFild));
    }
    public async Task DeleteCustomFild(int id)
    {
        var customFild = await _customFildRepository.GetCustomFildById(id);
        _checkerService.CheckIfCustomFildEmpty(customFild, id);
        await _customFildRepository.DeleteCustomFild(id);
    }
}
