using System.Net;
using System.Text.Json;
using Task_Tracker.BusinessLayer.Exceptions;

namespace Task_Tracker.API.Middleware;

public class ExceptionHandlerMiddleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityNotFoundException exception)
            {
                await HandleExceptionAsync(context, HttpStatusCode.NotFound, exception.Message);
            }
            //catch (UniquenessException exception)
            //{
            //    await HandleExceptionAsync(context, HttpStatusCode.UnprocessableEntity, exception.Message);
            //}
            //catch (DataException exception)
            //{
            //    await HandleExceptionAsync(context, HttpStatusCode.UnprocessableEntity, exception.Message);
            //}
            //catch (AccessException exception)
            //{
            //    await HandleExceptionAsync(context, HttpStatusCode.Forbidden, exception.Message);
            //}
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, HttpStatusCode.InternalServerError, ex.Message);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new { error = message });

            return context.Response.WriteAsync(result);
        }
    }
}
