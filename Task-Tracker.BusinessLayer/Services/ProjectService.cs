using AutoMapper;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.Exceptions;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Service;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;
    private readonly ICheckerService _checkerService;
    public ProjectService(IMapper mapper, IProjectRepository projectRepository, ICheckerService checkerService)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
        _checkerService = checkerService;
    }
    public async Task<int> AddProject(ProjectModel projectModel)
    {
        projectModel.CurrentStatus = CurrentStatusProject.NotStarted; 
        return await _projectRepository.AddProject(_mapper.Map<ProjectEntity>(projectModel));

    }

    public async Task DeleteProject(int id)
    {
        var project = await _projectRepository.GetProjectById(id);
        _checkerService.CheckIfProjectEmpty(project, id);
        await _projectRepository.DeleteProject(id);
    }

    public async Task<ProjectModel> GetProjectById(int id)
    {
        var project = await _projectRepository.GetProjectById(id);
        _checkerService.CheckIfProjectEmpty(project, id);
        return _mapper.Map<ProjectModel>(project);
    }

    public async Task<List<ProjectModel>> GetProjects()
    {
        return _mapper.Map<List<ProjectModel>>(await _projectRepository.GetProjects());
    }


    public async Task<List<TaskModel>> GetTasksByProjectId(int id)
    {
        var project = await _projectRepository.GetProjectById(id);
        _checkerService.CheckIfProjectEmpty(project, id);
        return _mapper.Map<List<TaskModel>>(project);
    }

    public async Task UpdateProject(ProjectModel newModel, int id)
    {
        var project = await _projectRepository.GetProjectById(id);
        _checkerService.CheckIfProjectEmpty(project, id);

        project.Name = newModel.Name;
        project.StartDate = newModel.StartDate;
        project.CompletionDate = newModel.CompletionDate;
        project.CurrentStatus = newModel.CurrentStatus;
        project.Priority = newModel.Priority;

        await _projectRepository.UpdateProject(_mapper.Map<ProjectEntity>(project));
       
    }
}
