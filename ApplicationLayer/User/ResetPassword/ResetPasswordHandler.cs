using ApplicationCommon;
using ApplicationCommon.MailPassword;
using AutoMapper;
using Domain;
using Domain.DomainObjects;
using Domain.ResponseValidations;
using MediatR;
using Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.ResetPassword
{
    public class ResetPasswordHandler : GeneratePassword, IRequestHandler<ResetPasswordCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        public ResetPasswordHandler(IUnitOfWork unitOfWork, IMapper mapper,IMailService mailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
        }

        public async Task<BaseResponse<object>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            bool email = await _unitOfWork.User.CheckEmail(request.EmailId);
            if (!email)
            {
                var error = new List<ResponseValidation>
                { 
                  new ResponseValidation("Incorrect Mail Id")
             };
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            string password =GenerateNewPassword();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            UserDetails userDetails = new UserDetails(null, null,null, null,hashedPassword, request.EmailId, null, null, null, null, null, null, DateTime.Today, request.LoggedUserName, null);
            await _unitOfWork.User.ResetPassword(userDetails);
            var emailBody = "Your Password Reset done.\n\n New Password : " + password;
            MailModel mail = new(request.EmailId, "LMS ACCOUNT PASSWORD", emailBody);
            await _mailService.SendEmailAsync(mail);

            return new BaseResponse<object>((object)null);
        }
    }
}
