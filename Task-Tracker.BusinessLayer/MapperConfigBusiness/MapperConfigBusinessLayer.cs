using AutoMapper;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.BusinessLayer.MapperConfig;

public class MapperConfigBusinessLayer : Profile
{
    public MapperConfigBusinessLayer()
    {
        CreateMap<ProjectEntity, ProjectModel>().ReverseMap();
            //.ForMember(t => t.CurrentStatus, act => act.MapFrom(l => l.Item1.Id));

        CreateMap<TaskEntity, TaskModel>().ReverseMap();
    }
}
