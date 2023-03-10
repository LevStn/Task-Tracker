using AutoMapper;
using Task_Tracker.API.Models.Requests;
using Task_Tracker.API.Models.Responses;
using Task_Tracker.BusinessLayer.Models;

namespace Task_Tracker.API.MapperConfiguration;

public class MapperApi : Profile
{
    public MapperApi()
    {
        CreateMap<ProjectRequest, ProjectModel>();
        CreateMap<TaskCreatingRequest, TaskModel>();
        CreateMap<TaskUpdatingRequest, TaskModel>();
        CreateMap<ProjectModel, ProjectResponse>();
        CreateMap<TaskModel, TaskResponse>()
            .ForMember(t => t.CustomFilds, dest => dest.MapFrom(p => p.CustomFildModels));
        CreateMap<CustomFildRequest, CustomFildModel>();
        CreateMap<CustomFildModel, CustomFildResponse>();
    }
}
