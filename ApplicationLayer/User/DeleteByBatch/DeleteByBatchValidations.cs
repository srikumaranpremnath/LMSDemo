using FluentValidation;

namespace Application.User.BatchDelete
{
    public class DeleteByBatchValidations : AbstractValidator<DeleteByBatchCommand>
    {
        public DeleteByBatchValidations()
        {
            RuleFor(task => task.BatchYear).NotEmpty().WithMessage("Batch Year is required");
        }
    }
}
