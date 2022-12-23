using Microsoft.EntityFrameworkCore;
using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer;

public class TaskTrackerContext : DbContext
{
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }

    public TaskTrackerContext(DbContextOptions<TaskTrackerContext> option)
       : base(option)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.ToTable("Project");
            entity.HasKey(c => c.Id);

           
        });

        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.ToTable("Task");
            entity.HasKey(c => c.Id);

            entity
               .HasOne(t => t.Project)
               .WithMany(a => a.Tasks);
        });
    }
}
