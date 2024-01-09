using AutoMapper;
using Domain.DomainObjects;
using Domain.ResponseValidations;
using Responses;

namespace Application.User.CreateUser
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDetails, UserDTO>().ForMember(destinationMember => destinationMember.Address, memberOptions => memberOptions.MapFrom(sourceMember => sourceMember.Address));
            CreateMap<ResponseValidation, ResponseErrors>();
        }
    }
}
