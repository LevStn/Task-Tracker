using Microsoft.OpenApi.Models;
using Task_Tracker.BusinessLayer.Service;
using Task_Tracker.DataLayer.Repositories;

namespace Task_Tracker.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IProjectService, ProjectService>();

    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProjectRepository, ProjectRepository>();
    }

   

    //public static void AddFluentValidation(this IServiceCollection services)
    //{
    //    services.AddFluentValidationAutoValidation(config => config.DisableDataAnnotationsValidation = true);
    //    services.AddScoped<IValidator<TransactionRequest>, TransactionRequestValidator>();
    //    services.AddScoped<IValidator<TransactionTransferRequest>, TransactionTransferRequestValidator>();
    //}

    public static void AddSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Task-Tracker",
                Version = "v1"
            });
        });
    }
}
