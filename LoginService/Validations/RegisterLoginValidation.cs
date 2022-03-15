using FluentValidation;
using LoginService.Commands;

namespace LoginService.Validations
{
    public class RegisterLoginValidation : AbstractValidator<RegisterLoginCommand>
    {
        public RegisterLoginValidation()
        {
            RuleFor(m => m.Password)
                .NotEmpty()
                .MinimumLength(8)
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

            RuleFor(m => m.Username).Cascade(CascadeMode.Stop)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);
                //.Must((m, cancellation) => accountRepository.UserNameExists(m.Username).Result == false)
                //.WithMessage("The provided Username has already been taken");
        }
    }
}