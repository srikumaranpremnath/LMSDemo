using FluentValidation;

namespace Application.Book.ReturnBook
{
    public class ReturnBookValidations : AbstractValidator<ReturnBookCommand>
    {
        public ReturnBookValidations()
        {
            RuleFor(task => task.BookCode).NotNull().NotEmpty().WithMessage("BookCode is Required");
        }

    }
}
