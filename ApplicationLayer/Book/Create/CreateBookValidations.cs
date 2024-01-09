using FluentValidation;

namespace Application.Book.CreateBook
{
    public class CreateBookValidations : AbstractValidator<CreateBookCommand>
    {
        public CreateBookValidations()
        {
            RuleFor(task => task.BookCode).NotEmpty().WithMessage("Book Code is required");
            RuleFor(task => task.BookName).NotEmpty().WithMessage("Book Name is required");
            RuleFor(task => task.RowId).NotEmpty().WithMessage("RowId is required");
            RuleFor(task => task.RackId).NotEmpty().WithMessage("RackId is required");
            RuleFor(task => task.BookDescription).SetValidator(new BookDescriptionValidations());
        }

    }

    public class BookDescriptionValidations : AbstractValidator<BookDescriptionCommand>
    {
        public BookDescriptionValidations()
        {
            RuleFor(task => task.AuthorName).NotEmpty().WithMessage("Author name is required");
            RuleFor(task => task.EditionNumber).NotEmpty().WithMessage("Edition Number is required");
            RuleFor(task => task.PublicationName).NotEmpty().WithMessage("Publication Name is required");

        }
    }
}