using AutoMapper;
using Domain;
using Domain.DomainObjects;
using Domain.ResponseValidations;
using MediatR;
using Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Application.User.DeleteUser
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (await _unitOfWork.User.CheckExistingRecords(request.UserDetailsId))
            {
                var error = new List<ResponseValidation>
                     {
                     new ResponseValidation("Can't Delete account, Return Books or Penality need to be cleared")
                     };
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            UserDetails userDetails = new UserDetails(request.UserDetailsId,null,null,null,null,null,null,null,null,null,null,null,DateTime.Today,request.LoggedUserName,null);
            await _unitOfWork.User.Delete(userDetails);
            return new BaseResponse<object>((object)null);
        }
    }
}
