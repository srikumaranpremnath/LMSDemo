using FluentValidation;

namespace Application.User.DeleteUser
{
    public class DeleteUserValidations : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidations()
        {
            RuleFor(task => task.UserDetailsId).NotEmpty().WithMessage("Either UserId or Roll number required");
            
        }
    }
}
