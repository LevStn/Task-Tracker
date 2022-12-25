using Microsoft.EntityFrameworkCore;
using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.DataLayer;

public class TaskTrackerContext : DbContext
{   
    public DbSet<ProjectEntity> Projects { get; set; }
    public DbSet<TaskEntity> Tasks { get; set; }
    public DbSet<CustomFildEntity> CustomFilds { get; set; }

    public TaskTrackerContext(DbContextOptions<TaskTrackerContext> option)
    : base(option)
    {
       
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProjectEntity>(entity =>
        {
            entity.ToTable("Project");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Name).HasMaxLength(255);

        });

        modelBuilder.Entity<TaskEntity>(entity =>
        {
            entity.ToTable("Task");
            entity.HasKey(t => t.Id);

            entity
               .HasOne(t => t.Project)
               .WithMany(p => p.Task);

            entity.Property(t => t.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<CustomFildEntity>(entity =>
        {
            entity.ToTable("CustomFild");
            entity.HasKey(c => c.Id);

            entity
               .HasOne(c => c.Task)
               .WithMany(t => t.CustomFilds);

            entity.Property(c => c.Name).HasMaxLength(255);
        });
    }
}
