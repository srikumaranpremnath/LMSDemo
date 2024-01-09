using AutoMapper;
using Domain;
using Domain.DomainObjects;
using MediatR;
using Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using Domain.DomainObjects.Enums;
using System.Collections.Generic;
using Domain.ResponseValidations;

namespace Application.User.UpdateUser
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse<object>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
           
            Address address = new Address(request.Address.AddressId,request.UserDetailsId, request.Address.HouseNo, request.Address.Street, request.Address.Area, request.Address.Pincode, request.Address.LandMark, request.Address.City, request.Address.State, request.Address.Country);
            UserDetails userDetails = new UserDetails(request.UserDetailsId, request.Name, request.Department, request.RollNum, null, request.Email
                                                      , request.Mobile,false, null,UserType.User,null,null,DateTime.Today,request.LoggedUserName,address);
            var error = new List<ResponseValidation>();
            if(await _unitOfWork.User.CheckUpdatedEmail(userDetails))
            {
                error.Add(new ResponseValidation("Email already exist"));
            }
            if (await _unitOfWork.User.CheckUpdatedMobile(userDetails))
            {
                error.Add(new ResponseValidation("Mobile Number already exist"));
            }
            if (error.Count > 0)
            {
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            await _unitOfWork.User.Update(userDetails);
            return new BaseResponse<object>((object)null);
        }
    }
    
}
