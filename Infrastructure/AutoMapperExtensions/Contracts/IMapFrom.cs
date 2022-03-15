using AutoMapper;

namespace Infrastructure.AutoMapperExtensions.Contracts
{
    public interface IMapFrom<T>
    {
        void CreateMap(Profile profile) => profile.CreateMap(GetType(), typeof(T));
    }
}