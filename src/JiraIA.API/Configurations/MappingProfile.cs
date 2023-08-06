using AutoMapper;
using JiraIA.Domain.DTOs;
using JiraIA.Domain.Models;

namespace JiraIA.API.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
            CreateMap<UserDTO, User>().ReverseMap();
            //CreateMap<IEnumerable<UserDTO>, IEnumerable<User>>().ReverseMap();
        }
    }
}
