using AutoMapper;
using Domain;
using Domain.DomainObjects;
using MediatR;
using Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.DomainObjects.Enums;
using ApplicationCommon;
using ApplicationCommon.MailPassword;

namespace Application.User.CreateUser
{
    public class CreateUserHandler : GeneratePassword,IRequestHandler<CreateUserCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;

        public CreateUserHandler(IUnitOfWork unitOfWork, IMapper mapper, IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
        }
        public async Task<BaseResponse<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Guid UserDetailsId = Guid.NewGuid();
            Guid AddressId = Guid.NewGuid();
            string password = GenerateNewPassword();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            bool test = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            Address address = new Address(AddressId, UserDetailsId, request.Address.HouseNo, request.Address.Street, request.Address.Area, request.Address.Pincode, request.Address.LandMark, request.Address.City, request.Address.State, request.Address.Country);
            UserDetails userDetails = new UserDetails(UserDetailsId,request.Name, request.Department, request.RollNum, hashedPassword, request.Email
                                                      , request.Mobile,true, null,UserType.User,DateTime.Today.Date,"admin",null, null,address);
            await _unitOfWork.User.Create(userDetails);
            var emailBody = "Your LMS account creates Sucessfully, Refer Credentials Below\n\n RollNum : " + request.RollNum + "\n\n Password : " + password;
            MailModel mail = new(request.Email,"LMS ACCOUNT PASSWORD",emailBody);
            await _mailService.SendEmailAsync(mail);
            return new BaseResponse<object>((object)null);
        }
    }
    
}
