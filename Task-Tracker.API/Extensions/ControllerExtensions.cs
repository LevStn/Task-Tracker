using Microsoft.AspNetCore.Mvc;

namespace Task_Tracker.API.Extensions;

public static class ControllerExtensions
{
    public static string GetRequestPath(this ControllerBase controller) =>
           $"{controller.Request?.Scheme}://{controller.Request?.Host.Value}{controller.Request?.Path.Value}";
}
