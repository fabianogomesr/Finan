using FluentValidation;
using Finan.Domain.Parameters;

namespace Finan.Service.Validators
{
    public class UserValidator : AbstractValidator<UserCommand>
    {
        public UserValidator()
        {
            RuleFor(c => c.UserName)
                .NotEmpty().WithMessage("Please enter the name.")
                .NotNull().WithMessage("Please enter the name.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Please enter the email.")
                .NotNull().WithMessage("Please enter the email.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Please enter the password.")
                .NotNull().WithMessage("Please enter the password.");
        }
    }
}
