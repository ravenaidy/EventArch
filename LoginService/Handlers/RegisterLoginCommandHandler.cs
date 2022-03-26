using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LoginService.Commands;
using LoginService.Models;
using LoginService.Repositories.Contracts;
using MediatR;

namespace LoginService.Handlers
{
    public class RegisterLoginCommandHandler : IRequestHandler<RegisterLoginCommand>
    {
        private readonly ILoginRepository _loginRepository;
        private readonly IMapper _mapper;

        public RegisterLoginCommandHandler(ILoginRepository loginRepository, IMapper mapper)
        {
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Unit> Handle(RegisterLoginCommand request, CancellationToken cancellationToken)
        {
            var login = _mapper.Map<Login>(request);
            await _loginRepository.RegisterLogin(login);
            return Unit.Value;
        }
    }
}