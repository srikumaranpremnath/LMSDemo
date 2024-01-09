using MediatR;
using Responses;

namespace Application.Authentication.Login
{
    public class LoginCommand : IRequest<BaseResponse<LoginDTO>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
