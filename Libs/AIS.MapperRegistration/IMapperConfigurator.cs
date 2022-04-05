using AutoMapper;

namespace AIS.MapperRegistration
{
    public interface IMapperConfigurator
    {
        void SetMapper(Profile profile);
    }
}