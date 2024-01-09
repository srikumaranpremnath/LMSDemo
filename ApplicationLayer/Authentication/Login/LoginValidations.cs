using FluentValidation;

namespace Application.Authentication.Login
{
    public class LoginValidations : AbstractValidator<LoginCommand>
    {
        public LoginValidations()
        {
            RuleFor(task => task.Username).NotNull().NotEmpty().WithMessage("Username is required");
            RuleFor(task => task.Password).NotNull().NotEmpty().WithMessage("Password is required");
        }
    }
}
