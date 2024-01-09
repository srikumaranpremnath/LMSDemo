using Domain.DomainObjects.Enums;

namespace Application.Authentication.Login
{
    public class LoginDTO
    {
        public string Username { get; set; }
        public UserType UserTypeId { get; set; }
        public string Token { get; set; }
    }
}
