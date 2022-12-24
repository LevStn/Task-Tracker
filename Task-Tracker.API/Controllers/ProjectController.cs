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
    [SwaggerOperation(Summary = "Write your summary here")]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> AddProject([FromBody] ProjectCreateRequest project)
    {
        var id = await _projectService.AddProject(_mapper.Map<ProjectModel>(project));
        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProjectEntity>> GetProjectById([FromRoute] int id)
    {
        return Ok(_mapper.Map<ProjectResponse>(await _projectService.GetProjectById(id)));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<ProjectResponse>>> GetProjects()
    {
        return Ok(_mapper.Map<List<ProjectResponse>>(await _projectService.GetProjects()));
    }

    [HttpGet("{id}/tasks")]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<TaskResponse>>> GetTasksByProjectId([FromRoute] int id)
    {
        var tasks = await _projectService.GetTasksByProjectId(id);
        return Ok(_mapper.Map<List<TaskResponse>>(tasks));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateProjectById([FromBody] ProjectUpdateRequest updateModel, [FromRoute] int id)
    {
        await _projectService.UpdateProject(_mapper.Map<ProjectModel>(updateModel), id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteProjectById([FromRoute] int id)
    {
        await _projectService.DeleteProject(id);
        return NoContent();
    }

    [HttpGet("id/sorting")]
    [ProducesResponseType(typeof(ProjectResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ProjectResponse>>> GetProjectsBySorting([FromRoute] TypeOFSorting typeOFSorting )
    {
        return Ok();
    }

}
