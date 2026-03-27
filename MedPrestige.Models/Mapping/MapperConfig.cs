using AutoMapper;

namespace MedPrestige.Models.Mapping
{
    public static class MapperConfig
    {
        public static void RegisterMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<MappingProfile>();
        }
    }
}
