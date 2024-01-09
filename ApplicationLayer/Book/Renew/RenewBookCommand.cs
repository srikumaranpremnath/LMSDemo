using MediatR;
using Responses;
using System;

namespace Application.Book.RenewBook
{
    public class RenewBookCommand : IRequest<BaseResponse<RenewBookDTO>>
    {
        public string BookCode { get; set; }

        public string LoggedUserName { get; set; }


    }
}
