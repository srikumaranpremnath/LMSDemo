using FluentValidation;

namespace Application.Book.GetNotReturnedByUser
{
    public class GetNotReturnedByUserValidations : AbstractValidator<GetNotReturnedByUserQuery>
    {
        public GetNotReturnedByUserValidations()
        {
            RuleFor(task => task.RollNum).NotEmpty().WithMessage("Roll Number is required");
        }
    }
}
