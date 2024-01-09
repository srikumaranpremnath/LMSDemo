using AutoMapper;
using Domain;
using Domain.DomainObjects;
using Domain.DomainObjects.Enums;
using MediatR;
using Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Book.IssueBook
{
    public class IssueBookHandler : IRequestHandler<IssueBookCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public IssueBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(IssueBookCommand request, CancellationToken cancellationToken)
        {
            Guid userDetailsId = await _unitOfWork.User.GetUserByRollNum(request.RollNum);
            Guid bookDetailsId = await _unitOfWork.Book.GetBookByBookCode(request.BookCode);
            int statusId = await _unitOfWork.Book.BookAvailabilityCheck(request.BookCode);
            BookIssue bookDetails = new BookIssue(Guid.NewGuid(),userDetailsId,bookDetailsId, DateTime.Today, DateTime.Today.AddDays(14),null,null,request.BookCode,null,null,null, ReturnedStatus.NotReturned, request.LoggedUserName,DateTime.Today.Date,null,null);
            var error =  bookDetails.CanIssue(statusId);
            if (error.Count > 0)
            {
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            await _unitOfWork.Book.IssueBook(bookDetails);
            return new BaseResponse<object>((object)null);
        }
    }
}
