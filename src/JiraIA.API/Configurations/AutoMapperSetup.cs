namespace JiraIA.API.Configurations
{
    public static class AutoMapperSetup
    {
        public static IServiceCollection AddAutoMapperSetup(this IServiceCollection services) 
        { 
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(MappingProfile));

            AutoMapperConfig.RegisterMappings();

            return services;
        }
    }
}
