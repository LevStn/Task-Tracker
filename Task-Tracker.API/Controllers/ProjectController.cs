using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Task_Tracker.API.Extensions;
using Task_Tracker.API.Models.Requests;
using Task_Tracker.API.Models.Responses;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Service;
using Task_Tracker.DataLayer.Entities;
using Task_Tracker.DataLayer.Enums;

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
    [SwaggerOperation(Summary = "Create new project")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> AddProject([FromBody] ProjectRequest project)
    {
        var id = await _projectService.AddProject(_mapper.Map<ProjectModel>(project));
        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Get project by id")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectEntity>> GetProjectById([FromRoute] int id)
    {
        return Ok(_mapper.Map<ProjectResponse>(await _projectService.GetProjectById(id)));
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all projects")]
    [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProjectResponse>>> GetProjects()
    {
        return Ok(_mapper.Map<List<ProjectResponse>>(await _projectService.GetProjects()));
    }

    [HttpGet("{id}/tasks")]
    [SwaggerOperation(Summary = "Get all tasks in project")]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<TaskResponse>>> GetTasksByProjectId([FromRoute] int id)
    {
        var tasks = await _projectService.GetTasksByProjectId(id);
        return Ok(_mapper.Map<List<TaskResponse>>(tasks));
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Changing project")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateProjectById([FromBody] ProjectRequest updateModel, [FromRoute] int id,
                                                                  [FromQuery] CurrentStatusProject currentStatus)
    {
        await _projectService.UpdateProject(_mapper.Map<ProjectModel>(updateModel), id, currentStatus);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Delete project")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProjectById([FromRoute] int id)
    {
        await _projectService.DeleteProject(id);
        return NoContent();
    }

    [HttpGet("id/sorting")]
    [SwaggerOperation(Summary = "Sorting projects by various parameters")]
    [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProjectResponse>>> GetProjectsBySorting([FromQuery] TypeOFSorting typeOFSorting)
    {
        return Ok(_mapper.Map<List<ProjectResponse>>(await _projectService.SortingProjectByParameters(typeOFSorting)));

    }

}
