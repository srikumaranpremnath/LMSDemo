using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Book.Update
{
    public class UpdateBookValidations : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidations()
        {
            RuleFor(task => task.BookDetailsId).NotEmpty().WithMessage("Book Details ID is required");
            RuleFor(task => task.BookCode).NotEmpty().WithMessage("Book Code is required");
            RuleFor(task => task.BookName).NotEmpty().WithMessage("Book Name is required");
            RuleFor(task => task.RowId).NotEmpty().WithMessage("RowId is required");
            RuleFor(task => task.RackId).NotEmpty().WithMessage("RackId is required");
            RuleFor(task => task.BookDescription).SetValidator(new BookDescriptionValidations());
        }

    }

    public class BookDescriptionValidations : AbstractValidator<UpdateBookDescriptionCommand>
    {
        public BookDescriptionValidations()
        {
            RuleFor(task => task.BookDescriptionId).NotEmpty().WithMessage("Book Description Id  is required");
            RuleFor(task => task.AuthorName).NotEmpty().WithMessage("Author name is required");
            RuleFor(task => task.EditionNumber).NotEmpty().WithMessage("Edition Number is required");
            RuleFor(task => task.PublicationName).NotEmpty().WithMessage("Publication Name is required");

        }
    }
}
