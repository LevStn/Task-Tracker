using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Task_Tracker.API.Extensions;
using Task_Tracker.API.MapperConfiguration;
using Task_Tracker.BusinessLayer.MapperConfig;
using Task_Tracker.DataLayer;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var result = new BadRequestObjectResult(context.ModelState);
            result.StatusCode = StatusCodes.Status422UnprocessableEntity;
            return result;
        };

    });
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAutoMapper(typeof(MapperApi), typeof(MapperConfigBusinessLayer));
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidation();

builder.Services.AddDbContext<TaskTrackerContext>(t =>
{
    t.UseSqlServer(@"Server=.;Database=Task-Tracking;Trusted_Connection=True;TrustServerCertificate=true");
});
var app = builder.Build();
app.UseCustomExceptionHandler();


if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
