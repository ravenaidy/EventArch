using AutoMapper;

namespace Infrastructure.AutoMapperExtensions.Contracts
{
    public interface IMapTo<T>
    {
        void CreateMap(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}