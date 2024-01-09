
using System;

namespace Application.User
{
    public class UserDTO
    {
         public string Name { get;  set; }

        public string Department { get;  set; }

        public string RollNum { get;  set; }
        public string Password { get;  set; }
        public string Email { get;  set; }
        public string Token { get; set; }
        public long? Mobile { get;  set; }

        public AddressDTO Address { get;  set; }
        public bool? IsDeleted { get;  set; }

        public bool? IsFirstLogin { get;  set; }

      
    }

    public class AddressDTO
    {
        public Guid? AddressId { get;  set; }
        public Guid? UserDetailsId { get;  set; }
        public string HouseNo { get;  set; }

        public string Street { get;  set; }

        public string Locality { get;  set; }
        public int Pincode { get;  set; }
        public string LandMark { get;  set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }



    }
    
}
