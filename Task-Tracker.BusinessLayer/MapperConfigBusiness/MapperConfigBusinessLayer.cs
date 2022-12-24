using AutoMapper;
using Task_Tracker.BusinessLayer.Models;
using Task_Tracker.DataLayer.Entities;

namespace Task_Tracker.BusinessLayer.MapperConfig;

public class MapperConfigBusinessLayer : Profile
{
    public MapperConfigBusinessLayer()
    {
        CreateMap<ProjectEntity, ProjectModel>().ReverseMap();
        CreateMap<TaskEntity, TaskModel>()
           .ForMember(t => t.ProjectId, dest => dest.MapFrom(p => p.Project.Id))
           .ForMember(t=>t.CustomFildModels,dest => dest.MapFrom(p=>p.CustomFilds))
           .ReverseMap();
        CreateMap<CustomFildEntity, CustomFildModel>().ReverseMap();
            //.ForMember(t => t.TaskId, dest => dest.MapFrom(p => p.Task.Id)).ReverseMap();

    }
}
