
using MediatR;
using Responses;
using System;

namespace Application.User.CreateUser
{
    public class CreateUserCommand : IRequest<BaseResponse<object>>
    {
        public string Name { get;  set; }
        public string Department { get;  set; }
        public string RollNum { get;  set; }
        public string Email { get;  set; }
        public long Mobile { get;  set; }
        public CreateAddressCommand Address { get;  set; }
        public string LoggedUserName { get; set; }





    }

    public class CreateAddressCommand
    {
        public string HouseNo { get;  set; }

        public string Street { get;  set; }

        public string Area { get;  set; }
        public int Pincode { get;  set; }
        public string LandMark { get;  set; }
        public string City { get; set; }
        public string State { get;  set; }
        public string Country { get; set; }



    }

}
