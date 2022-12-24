using Microsoft.EntityFrameworkCore;
using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly TaskTrackerContext _context;

    public ProjectRepository(TaskTrackerContext context)
    {
        _context = context;
    }

    public async Task<int> AddProject(ProjectEntity project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();

        return project.Id;
    }

    public async Task<ProjectEntity?> GetProjectById(int id) => await _context.Projects
        .FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted == false);


    public async Task<List<ProjectEntity>> GetProjects() => await _context.Projects
        .Where(p => p.IsDeleted == false)
        .ToListAsync();

    public async Task<List<TaskEntity>> GetTasksByProjectId(int id)=> await _context.Tasks
        .Include(t => t.Project)
        .Include(t => t.CustomFilds.Where(t => t.IsDeleted == false))
        .Where(t => t.Project.Id == id && t.IsDeleted == false).ToListAsync();

    public async Task UpdateProject(ProjectEntity project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProject(int id)
    {
        var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == id);
        project!.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}
