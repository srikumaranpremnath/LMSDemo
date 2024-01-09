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

namespace Application.Book.RenewBook
{
    public class RenewBookHandler : IRequestHandler<RenewBookCommand, BaseResponse<RenewBookDTO>>
    {


        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RenewBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

        }
        public async Task<BaseResponse<RenewBookDTO>> Handle(RenewBookCommand request, CancellationToken cancellationToken)
        {
            Guid bookDetailsId = await _unitOfWork.Book.GetBookByBookCode(request.BookCode);
            Guid bookIssueID = await _unitOfWork.Book.GetBookIssueByBookDetailsId(bookDetailsId);
            BookIssue bookDetails = new BookIssue(bookIssueID, null, bookDetailsId, null, DateTime.Today.AddDays(7), null, DateTime.Today, request.BookCode,null,null,null,ReturnedStatus.NotReturned,null, null,request.LoggedUserName, DateTime.Today.Date);
            DateTime expectedReturnDate = await _unitOfWork.Book.GetExpectedReturnDate(bookIssueID);
            var error =  bookDetails.CanRenew(expectedReturnDate);
            if (error.Count > 0)
            {
                return new BaseResponse<RenewBookDTO>(_mapper.Map<List<ResponseErrors>>(error));
            }
            await _unitOfWork.Book.RenewBook(bookDetails);
            return new BaseResponse<RenewBookDTO>(_mapper.Map<RenewBookDTO>(bookDetails));
        }
    }
}
