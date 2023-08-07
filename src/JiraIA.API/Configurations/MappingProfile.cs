using AutoMapper;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Models;

namespace JiraIA.API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDTO, User>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = src.Id == null ? Guid.NewGuid().ToString() : src.Id;
                    dest.CreatedAt = (DateTime)(src.CreatedAt == null ? DateTime.UtcNow : src.CreatedAt);
                })
                .ReverseMap();

            CreateMap<BoardStatusDTO, BoardStatus>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = src.Id == null ? Guid.NewGuid().ToString() : src.Id;
                    dest.IsDeleted = false;
                })
                .ReverseMap();

            CreateMap<TaskDTO, TaskModel>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = src.Id == null ? Guid.NewGuid().ToString() : src.Id;
                })
                .ReverseMap();
        }
    }
}
