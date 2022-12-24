using static Task_Tracker.API.Middleware.ExceptionHandlerMiddleware;

namespace Task_Tracker.API.Extensions;

public static class ApplicationBuildeExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}
