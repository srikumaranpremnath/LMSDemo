using MediatR;
using Responses;
using System.Collections.Generic;

namespace Application.Book.GetNotReturnedByUser
{
    public class GetNotReturnedByUserQuery : IRequest<BaseResponse<List<BookIssueDTO>>>
    {
        public string RollNum { get; set; }
    }
}
