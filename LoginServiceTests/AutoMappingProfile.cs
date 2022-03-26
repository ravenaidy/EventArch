using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Infrastructure.AutoMapperExtensions.Contracts;
using LoginService.Commands;
using LoginService.Models;

namespace LoginServiceTests
{
    public class AutoMappingProfile : Profile
    {
        private const string MethodName = "CreateMap";

        public AutoMappingProfile()
        {
            var types = new[] {Assembly.GetAssembly(typeof(Login)), Assembly.GetAssembly(typeof(RegisterLoginCommand)) }
                .SelectMany(x => x?.GetExportedTypes() ?? Type.EmptyTypes)
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

                methodInfo?.Invoke(instance, new object[] {this});
            }
        }
    }
}