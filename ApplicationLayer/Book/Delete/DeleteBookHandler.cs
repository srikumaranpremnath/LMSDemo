using AutoMapper;
using Domain;
using Domain.DomainObjects;
using Domain.DomainObjects.Enums;
using Domain.ResponseValidations;
using MediatR;
using Responses;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Book.DeleteBook
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand,BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DeleteBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponse<object>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            Guid bookDetailsId = await _unitOfWork.Book.GetBookByBookCode(request.BookCode);
            if (await _unitOfWork.Book.HasExistingRecords(bookDetailsId))
            {
                var error = new List<ResponseValidation>
                {
                    new ResponseValidation("This Book is issued to some user, can't delete")
                };
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            BookDetails bookDetails = new BookDetails(null, null, request.BookCode, null, null, BookStatus.Unavailable, null, null, request.LoggedUserName, DateTime.Today, null);
            await _unitOfWork.Book.Delete(bookDetails);
            return new BaseResponse<object>((object)null);
        }
    }
}
