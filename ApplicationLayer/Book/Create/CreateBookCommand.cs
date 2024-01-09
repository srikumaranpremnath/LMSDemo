using Domain.DomainObjects.Enums;
using MediatR;
using Responses;

namespace Application.Book.CreateBook
{
    public class CreateBookCommand : IRequest<BaseResponse<object>>
    {
        public string BookCode { get; set; }
        public string BookName { get; set; }
        public int RackId { get; set; }
        public int RowId { get; set; }
        public BookDescriptionCommand BookDescription { get; set; }
        public string LoggedUserName { get; set; }
    }
    public class BookDescriptionCommand
    {
        public string AuthorName { get;  set; }
        public string PublicationName { get;  set; }
        public int EditionNumber { get;  set; }
    }


}
