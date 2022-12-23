using Microsoft.AspNetCore.Mvc;
using Task_Tracker.API.Extensions;
using Task_Tracker.API.Models.Requests;
using Task_Tracker.API.Models.Responses;

namespace Task_Tracker.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<int>> AddTask([FromBody] TaskCreateRequest createRequest)
    {
        var id = 1; // test
        return Created($"{this.GetRequestPath()}/{id}", id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskResponse>> GetTaskById([FromRoute] int id)
    {
        return Ok(new TaskResponse());
    }

    [HttpGet]
    public async Task<ActionResult<List<TaskResponse>>> GetTasks()
    {
        return Ok(new List<TaskResponse>());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTaskById([FromBody] TaskUpdateRequest updateRequest, [FromRoute] int id)
    {
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTaskById([FromRoute] int id)
    {
        return NoContent();
    }
}
