using AutoMapper;
using Domain.DomainObjects;
using Domain;
using Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Authentication.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, BaseResponse<LoginDTO>>
    {
    
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJsonWebToken _jsonWebToken;
        public LoginHandler(IUnitOfWork unitOfWork, IMapper mapper, IJsonWebToken jsonWebToken)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jsonWebToken = jsonWebToken;
        }

        public async Task<BaseResponse<LoginDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loggedUserDetails = await _unitOfWork.User.Login(request.Username);
            UserDetails userDetails = new(null,null,null,request.Username,request.Password,null,null,null,null,null, null, null, null, null, null);
            var error = userDetails.IsValidLogin(loggedUserDetails);
            if (error.Count>0)
            {
                return new BaseResponse<LoginDTO>(_mapper.Map<List<ResponseErrors>>(error));
            }
            userDetails.Token = _jsonWebToken.GenerateJWT(request.Username,loggedUserDetails.UserTypeId.ToString());
            await _unitOfWork.User.LoadPenality(request.Username);
            return new BaseResponse<LoginDTO>(_mapper.Map<LoginDTO>(userDetails));
        }

       
    }
}
