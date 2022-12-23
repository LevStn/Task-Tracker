﻿using Task_Tracker.API.Enums;

namespace Task_Tracker.API.Models.Responses;

public class TaskResponse
{
    public string Name { get; set; }
    public CurrentStatusTask CurrentStatus { get; set; }
    public string Discriptions { get; set; }
    public int Priority { get; set; }
}
