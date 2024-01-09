using FluentValidation;

namespace Application.User.ChangePassword
{
    public class ChangePassWordValidations : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePassWordValidations()
        {
            RuleFor(task => task.UserDetailsId).NotEmpty().WithMessage("UserDetailsId is Required");
            RuleFor(task => task.OldPassword).NotEmpty().WithMessage("Old Password is required");
            RuleFor(task => task.NewPassword).NotEmpty().WithMessage("NewPassword is Required");
        }
    }
}
