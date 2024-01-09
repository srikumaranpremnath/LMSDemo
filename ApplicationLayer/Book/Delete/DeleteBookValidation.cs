using FluentValidation;
 using System.Text;

namespace Application.Book.DeleteBook
{
    public class DeleteBookValidation : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookValidation()
        {
            RuleFor(task => task.BookCode).NotEmpty().WithMessage("Book Code Required");
        }
    }
}
