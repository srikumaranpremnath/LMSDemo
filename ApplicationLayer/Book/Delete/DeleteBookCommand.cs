using MediatR;
using Responses;

namespace Application.Book.DeleteBook
{
    public class DeleteBookCommand: IRequest<BaseResponse<object>>
    {
        public string BookCode { get; set; }
        public string LoggedUserName { get; set; }
    }
}
