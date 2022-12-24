using Microsoft.EntityFrameworkCore;
using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskTrackerContext _context;

    public TaskRepository(TaskTrackerContext context)
    {
        _context = context;
    }

    public async Task<long> AddTask(TaskEntity task)
    {
        task.Project = await _context.Projects.FirstOrDefaultAsync(t => t.Id == task.Project.Id);
        await _context.Tasks.AddAsync(task);
        await _context.SaveChangesAsync();

        return task.Id;
    }

    public async Task<TaskEntity?> GetTaskById(long id) => await _context.Tasks
        .Include(t => t.CustomFilds.Where(t => t.IsDeleted == false))
        .Include(t => t.Project)
        .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);

    public async Task UpdateTask(TaskEntity newModel)
    {
        _context.Tasks.Update(newModel);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteTask(long id) 
    {
        var task = await _context.Tasks.FirstOrDefaultAsync(p => p.Id == id);
        task!.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}
