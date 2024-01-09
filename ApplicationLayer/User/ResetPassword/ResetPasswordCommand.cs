using MediatR;
using Responses;

namespace Application.User.ResetPassword
{
    public class ResetPasswordCommand : IRequest<BaseResponse<object>>
    {
        public string EmailId { get; set; }
        public string LoggedUserName { get; set; }


    }
}
