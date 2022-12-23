using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task_Tracker.API.Extensions;
using Task_Tracker.API.Models.Requests;
using Task_Tracker.API.Models.Responses;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Service;

namespace Task_Tracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProjectService _projectService;

    public ProjectController(IMapper mapper, IProjectService projectService)
    {
        _mapper = mapper;
        _projectService = projectService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    public async Task<ActionResult<int>> AddProject([FromBody] ProjectCreateRequest project)
    {
        var id =await _projectService.AddProject(_mapper.Map<ProjectModel>(project));
        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<ProjectResponse>> GetProjectById([FromRoute] int id)
    {
        var project = _projectService.GetProjectById(id);
        return Ok(_mapper.Map<ProjectResponse>(project));
    }

    [HttpGet]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProjectResponse>>> GetProjects()
    {
        var projects = _projectService.GetProjects();
        return Ok(_mapper.Map<List<ProjectResponse>>(projects));
    }

    [HttpGet("{id}/tasks")]
    [ProducesResponseType(typeof(long), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<TaskResponse>>> GetTasksByProjectId([FromRoute] int id)
    {
        var tasks = _projectService.GetTasksByProjectId(id);
        return Ok(_mapper.Map<List<TaskResponse>>(tasks));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateProjectById([FromBody] ProjectUpdateRequest updateModel, [FromRoute] int id)
    {
        await _projectService.UpdateProject(_mapper.Map<ProjectModel>(updateModel), id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteProjectById([FromRoute] int id)
    {
        await _projectService.DeleteProject(id);
        return NoContent();
    }
}
