using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Task_Tracker.API.Extensions;
using Task_Tracker.API.Models.Requests;
using Task_Tracker.API.Models.Responses;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.BusinessLayer.Services;

namespace Task_Tracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ITaskService _taskService;
    private readonly ICustomFildService _customFildService;


    public TaskController(IMapper mapper, ITaskService taskService, ICustomFildService customFildService)
    {
        _mapper = mapper;
        _taskService = taskService;
        _customFildService = customFildService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(long), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<long>> AddTask([FromBody] TaskCreateRequest createRequest)
    {
        var id = await _taskService.AddTask(_mapper.Map<TaskModel>(createRequest));
        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskResponse>> GetTaskById([FromRoute] long id)
    {
        return Ok(_mapper.Map<TaskResponse>(await _taskService.GetTaskById(id)));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> UpdateTaskById([FromBody] TaskUpdateRequest updateRequest, [FromRoute] int id)
    {
        await _taskService.UpdateTask(_mapper.Map<TaskModel>(updateRequest), id);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteTaskById([FromRoute] long id)
    {
        await _taskService.DeleteTask(id);
        return NoContent();
    }

    [HttpPost("id/custom-filds")]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status422UnprocessableEntity)]
    public async Task<ActionResult<int>> AddCustomFild([FromBody] CustomFildRequest customFild)
    {
        var id = await _customFildService.AddCustomFild(_mapper.Map<CustomFildModel>(customFild));
        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    [HttpDelete("{id}/custom-filds")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void),StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteCustomFild([FromRoute] int id)
    {
        await _customFildService.DeleteCustomFild(id);
        return NoContent();
    }
}
