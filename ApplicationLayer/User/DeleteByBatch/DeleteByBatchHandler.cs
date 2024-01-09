using AutoMapper;
using Domain;
using Domain.ResponseValidations;
using MediatR;
using Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.BatchDelete
{
    public class DeleteByBatchHandler : IRequestHandler<DeleteByBatchCommand,BaseResponse<object>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteByBatchHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<BaseResponse<object>> Handle(DeleteByBatchCommand request, CancellationToken cancellationToken)
        {
            bool CanBatchDelete = await _unitOfWork.User.CheckBatchDelete(request.BatchYear);
            if(CanBatchDelete)
            {
                var error = new List<ResponseValidation>(){
                    new ResponseValidation("Book Issued or Penality uncleared students still ")
                };
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            await _unitOfWork.User.DeleteUserByBatch(request.BatchYear);
            return new BaseResponse<object>((object)null);
        }
    }
}
