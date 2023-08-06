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
                    dest.Id = Guid.NewGuid().ToString();
                    dest.CreatedAt = DateTime.UtcNow;
                })
                .ReverseMap();

            CreateMap<BoardStatusDTO, BoardStatus>()
                .AfterMap((src, dest) =>
                {
                    dest.Id = Guid.NewGuid().ToString();
                    dest.IsDeleted = false;
                })
                .ReverseMap();
        }
    }
}
