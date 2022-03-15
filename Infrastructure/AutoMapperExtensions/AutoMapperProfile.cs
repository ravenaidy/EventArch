using System;
using System.Linq;
using AutoMapper;
using Infrastructure.AutoMapperExtensions.Contracts;

namespace Infrastructure.AutoMapperExtensions
{
    public class AutoMapperProfile : Profile
    {
        private const string MethodName = "CreateMap";

        public AutoMapperProfile()
        {
            var types = StartupExtensions.Assemblies
                .SelectMany(x => x.GetExportedTypes())
                .Where(x =>
                    x.IsClass &&
                    !x.IsAbstract &&
                    x.GetInterfaces()
                        .Any(i =>
                            i.IsGenericType &&
                            (
                                i.GetGenericTypeDefinition() == typeof(IMapFrom<>) ||
                                i.GetGenericTypeDefinition() == typeof(IMapTo<>)
                            )
                        )
                )
                .ToArray();

            foreach (Type type in types)
            {
                var instance = Activator.CreateInstance(type);
                var methodInfo =
                    type.GetMethod(MethodName) ??
                    type.GetInterface("IMapFrom`1")?.GetMethod(MethodName) ??
                    type.GetInterface("IMapTo`1")?.GetMethod(MethodName);

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}