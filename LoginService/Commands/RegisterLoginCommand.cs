using Infrastructure.AutoMapperExtensions.Contracts;
using LoginService.Models;
using MediatR;

namespace LoginService.Commands
{
    public record RegisterLoginCommand : IRequest, IMapTo<Login>
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
