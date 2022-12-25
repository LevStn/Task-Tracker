using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Task_Tracker.API.FluentValidators;
using Task_Tracker.API.Models.Requests;
using Task_Tracker.API.Validators;
using Task_Tracker.BusinessLayer.Checkers;
using Task_Tracker.BusinessLayer.Service;
using Task_Tracker.BusinessLayer.Services;
using Task_Tracker.DataLayer;
using Task_Tracker.DataLayer.Repositories;
using Microsoft.Extensions.Configuration;
namespace Task_Tracker.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ITaskService, TaskService>();
        services.AddScoped<ICustomFildService, CustomFildService>();
        services.AddScoped<ICheckerService, CheckerService>();
;
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();
        services.AddScoped<ICustomFildRepository, CustomFildRepository>();
    }



    public static void AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
        services.AddScoped<IValidator<CustomFildRequest>, CustomFildRequestValidator>();
        services.AddScoped<IValidator<ProjectRequest>, ProjectRequestValidator>();
        services.AddScoped<IValidator<TaskCreatingRequest>, TaskRequestValidator>();
    }

    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.EnableAnnotations();

            options.UseInlineDefinitionsForEnums();
           
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Task-Tracker",
                Version = "v1"
            });
            
        });
    }

    //public static void ConfigureServices(this IServiceCollection services)
    //{
    //    builder.Services

    //    var connString = new DbConfig();
    //    services.AddDbContext<TaskTrackerContext>(t =>
    //    {

    //        builder.Configuration.Bind(DbConfig.TASK_TRACKER_CONNECTION_STRING);
    //        t.UseSqlServer(DbConfig.TASK_TRACKER_CONNECTION_STRING);
    //        services.AddDbContext<TaskTrackerContext>(options =>
    //        options.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase")));
    //}
}
