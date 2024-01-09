using Domain.DomainObjects.Enums;
using Domain.ResponseValidations;
using System;
using System.Collections.Generic;

namespace Domain.DomainObjects
{
    public class UserDetails : Audit
    {
        public UserDetails()
        {

        }
        public UserDetails(Guid? userDetailsId, string name, string department, string rollNum, string password,string email, long? mobile,
                            bool? isFirstLogin,string token,UserType? userTypeId,DateTime? createdDate, string createdBy,DateTime? editedDate,string editedBy,
                            #nullable enable
                            Address? address)
        {
            UserDetailsId = userDetailsId;
            Name = name;
            Department = department;
            RollNum = rollNum;
            Password = password;
            Email = email;
            Mobile = mobile;
            UserTypeId = userTypeId;
            Address = address;
            IsFirstLogin = isFirstLogin;
            Token = token;
            CreatedBy = createdBy;
            CreatedDate = createdDate;
            EditedBy = editedBy;
            EditedDate = editedDate;
        }
        public Guid? UserDetailsId { get; private set; }
        public string Name { get; private set; }
        public string Department { get; private set; }
        public string RollNum { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public string Token { get; set; }
        public UserType? UserTypeId { get; set; }
        #nullable enable
        public Address? Address { get; set; }
        public long? Mobile { get; private set; }

        public bool? IsFirstLogin { get; private set; }

        public IReadOnlyList<ResponseValidation> IsValidLogin(UserDetails loggedUser)
        {
            var error = new List<ResponseValidation>();

               
                if (loggedUser == null || !BCrypt.Net.BCrypt.Verify(Password, loggedUser.Password))
                {
                    error.Add(new ResponseValidation("Invalid Credentials"));

                }
            
            return error;
        }
       
    }

    public class Address
    {
        public Address(Guid? addressId,Guid? userDetailsId,string houseNo,string street,string area,int? pincode,
                       string landMark,string city,string state,string country)
        {
            AddressId = addressId;
            UserDetailsId = userDetailsId;
            HouseNo = houseNo;
            Street = street;
            Area = area;
            Pincode = pincode;
            LandMark = landMark;
            City = city;
            State = state;
            Country = country;

        }
        public Guid? AddressId { get; private set; }
        public Guid? UserDetailsId { get; private set; }
        public string HouseNo { get; private set; }

        public string Street { get; private set; }

        public string Area { get; private set; }
        public int? Pincode { get; private set; }
        public string LandMark { get; private set; }
        public string City { get; set; }
        public string State { get; private set; }
        public string Country { get; set; }

     

    }
}
