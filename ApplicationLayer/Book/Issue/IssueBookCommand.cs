using MediatR;
using System;
using Responses;

namespace Application.Book.IssueBook
{
    public class IssueBookCommand : IRequest<BaseResponse<object>>
    {
        public string RollNum { get;  set; }
        public string BookCode { get;  set; }

        public string LoggedUserName { get; set; }
    }
}
