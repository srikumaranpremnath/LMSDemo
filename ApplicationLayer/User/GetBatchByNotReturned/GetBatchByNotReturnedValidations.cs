using FluentValidation;

namespace Application.User.GetBatchByNotReturned
{
    public class GetBatchByNotReturnedValidations : AbstractValidator<GetBatchByNotReturnedQuery>
    {
        public GetBatchByNotReturnedValidations()
        {
            RuleFor(task => task.batchYear).NotEmpty().WithMessage("Batch Year is required");
        }
    }
}
