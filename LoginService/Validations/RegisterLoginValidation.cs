using System;
using FluentValidation;
using LoginService.Commands;
using LoginService.Repositories.Contracts;

namespace LoginService.Validations
{
    public class RegisterLoginValidation : AbstractValidator<RegisterLoginCommand>
    {
        private readonly ILoginRepository _loginRepository;

        public RegisterLoginValidation(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository ?? throw new ArgumentNullException(nameof(loginRepository));
            
            RuleFor(m => m.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

            RuleFor(m => m.Username).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Must((m, cancellation) => _loginRepository.UserNameExists(m.Username).Result == false)
                .WithMessage("The provided Username has already been taken");
        }
    }
}