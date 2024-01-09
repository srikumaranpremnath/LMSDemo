using MediatR;
using Responses;
using System.Collections.Generic;

namespace Application.Book.SearchBook
{
    public class SearchBookQuery : IRequest<BaseResponse<List<BookDTO>>>
    {
        public string Keyword { get; set; }
    }
}
