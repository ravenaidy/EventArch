using AutoMapper;

namespace Infrastructure.AutoMapperExtensions.Contracts
{
    public interface IMapTo<T>
    {
        void CreateMap(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}