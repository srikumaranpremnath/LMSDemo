using AutoMapper;
using Domain.DomainObjects;

namespace Application.Authentication.Login
{
    public class LoginProfile :Profile
    {
        public LoginProfile()
        {
            CreateMap<UserDetails, LoginDTO>().ForMember(destinationMember => destinationMember.Username,
                                                         memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.RollNum)
                                                        );
                
        }
        
    }
}
