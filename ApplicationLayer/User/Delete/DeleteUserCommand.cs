using MediatR;
using Responses;
using System;

namespace Application.User.DeleteUser
{
    public class DeleteUserCommand : IRequest<BaseResponse<object>>
    {
        public Guid UserDetailsId { get; set; }
        public string LoggedUserName { get; set; }


    }
}
