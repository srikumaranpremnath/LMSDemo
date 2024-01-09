using FluentValidation;

namespace Application.User.GetByRollNum
{
    public class GetByRollNumValidations : AbstractValidator<GetByRollNumQuery>
    {
        public GetByRollNumValidations()
        {
            RuleFor(task => task.RollNum).NotEmpty().WithMessage("RollNumber Required");
        }
    }
}
