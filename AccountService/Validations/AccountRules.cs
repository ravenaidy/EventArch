using AccountService.Commands;
using FluentValidation;

namespace AccountService.Validations
{
    public class AccountRules : AbstractValidator<RegisterAccountCommand>
    {
        public AccountRules()
        {
            
        }
    }
}