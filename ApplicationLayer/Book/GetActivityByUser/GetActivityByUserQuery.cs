using MediatR;
using Responses;
using System;
using System.Collections.Generic;

namespace Application.Book.GetActivityByUser
{
    public class GetActivityByUserQuery : IRequest<BaseResponse<List<BookIssueDTO>>>
    {
        public string RollNum { get; set; }
    }
}
