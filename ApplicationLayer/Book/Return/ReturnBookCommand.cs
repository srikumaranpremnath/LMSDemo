using MediatR;
using Responses;

namespace Application.Book.ReturnBook
{
    public class ReturnBookCommand : IRequest<BaseResponse<object>>
    {
        public string BookCode { get; set; }

        public string LoggedUserName { get; set; }
    }
}
