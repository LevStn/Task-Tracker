using AutoMapper;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.BusinessLayer.Service;

public class ProjectService : IProjectService
{
    private readonly IMapper _mapper;
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IMapper mapper, IProjectRepository projectRepository)
    {
        _mapper = mapper;
        _projectRepository = projectRepository;
    }
    public async Task<int> AddProject(ProjectModel projectModel)
    {
        projectModel.CurrentStatus = CurrentStatusProject.NotStarted;
        var id = await _projectRepository.AddProject(_mapper.Map<ProjectEntity>(projectModel));
        return id;

    }

    public async Task DeleteProject(int id)
    {
        _projectRepository.DeleteProject(id);
    }

    public async Task<ProjectModel> GetProjectById(int id)
    {
        return _mapper.Map<ProjectModel>(await _projectRepository.GetProjectById(id));
    }

    public async Task<List<ProjectModel>> GetProjects()
    {
        return _mapper.Map<List<ProjectModel>>(await _projectRepository.GetProjects());
    }
    public async Task<List<TaskModel>> GetTasksByProjectId(int id)
    {
        return _mapper.Map<List<TaskModel>>(await _projectRepository.GetTasksByProjectId(id));
    }
    public async Task UpdateProject(ProjectModel newModel, int id)
    {
        await _projectRepository.UpdateProject(_mapper.Map<ProjectEntity>(newModel));
    }
}
