using AutoMapper;
using Domain;
using MediatR;
using Responses;
using System.Threading;
using System.Threading.Tasks;
using BCrypt.Net;
using System.Collections.Generic;
using Domain.ResponseValidations;

namespace Application.User.ChangePassword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand,BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChangePasswordHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            string oldPassword = await _unitOfWork.User.GetPassword(request.UserDetailsId);
            if (BCrypt.Net.BCrypt.Verify(request.OldPassword,oldPassword))
            {
                string newPassword = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
                await _unitOfWork.User.ChangePassword(newPassword,request.UserDetailsId);
                return new BaseResponse<object>((object)null);
            }
            var error = new List<ResponseValidation>
            {
                new ResponseValidation("Incorrect old Password")
            };
           return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
        }
    }
}
