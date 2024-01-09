using Domain;
using Domain.DomainObjects;
using Domain.DomainObjects.Enums;
using MediatR;
using Responses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Book.ReturnBook
{
    public class ReturnBookHandler : IRequestHandler<ReturnBookCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ReturnBookHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public async Task<BaseResponse<object>> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {

            Guid bookDetailsId = await _unitOfWork.Book.GetBookByBookCode(request.BookCode);
            Guid bookIssueID = await _unitOfWork.Book.GetBookIssueByBookDetailsId(bookDetailsId);
            BookIssue bookDetails = new BookIssue(bookIssueID,null, bookDetailsId, null,null,DateTime.Today, null,request.BookCode,null,null, null,ReturnedStatus.Returned, null,null, request.LoggedUserName, DateTime.Today.Date);
            await _unitOfWork.Book.ReturnBook(bookDetails);
            return new BaseResponse<object>((object)null);

        }
    }
}
