

using FluentValidation;

namespace Application.Book.IssueBook
{
    public class IssueBookValidations : AbstractValidator<IssueBookCommand>
    {
        public IssueBookValidations()
        {
            RuleFor(task => task.RollNum).NotEmpty().NotNull().WithMessage("Student RollNum Required");
            RuleFor(task => task.BookCode).NotEmpty().NotNull().WithMessage("Book Code  Required");

        }
    }
}
