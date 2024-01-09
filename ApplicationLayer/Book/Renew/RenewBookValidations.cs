using FluentValidation;

namespace Application.Book.RenewBook
{
    public class RenewBookValidations : AbstractValidator<RenewBookCommand>
    {
        public RenewBookValidations()
        {
            RuleFor(task => task.BookCode).NotNull().NotEmpty().WithMessage("BookCode is Required");

        }

    }
}
