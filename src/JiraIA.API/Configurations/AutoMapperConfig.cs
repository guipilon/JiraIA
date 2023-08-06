using AutoMapper;
using AutoMapper.Internal;

namespace JiraIA.API.Configurations
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg => 
            {
                cfg.AllowNullDestinationValues = true;
                cfg.AllowNullCollections = true;

                cfg.DisableConstructorMapping();

                cfg.AddProfile(new MappingProfile());
            });
        }
    }
}
