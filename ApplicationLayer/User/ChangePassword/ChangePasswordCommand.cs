using MediatR;
using Responses;
using System;

namespace Application.User.ChangePassword
{
    public class ChangePasswordCommand : IRequest<BaseResponse<object>>
    {
        public Guid UserDetailsId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public string LoggedUserName { get; set; }


    }
}
