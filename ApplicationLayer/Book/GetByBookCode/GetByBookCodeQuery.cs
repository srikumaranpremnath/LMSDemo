using MediatR;
using Responses;
using System.Collections.Generic;

namespace Application.Book.GetByBookCode
{
    public class GetByBookCodeQuery : IRequest<BaseResponse<List<BookDTO>>>
    {
        public string BookCode { get; set; }
    }
}
