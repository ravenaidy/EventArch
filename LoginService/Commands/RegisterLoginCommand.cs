using MediatR;

namespace LoginService.Commands
{
    public record RegisterLoginCommand : IRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
