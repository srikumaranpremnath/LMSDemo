using Domain.DomainObjects;
using Domain;
using MediatR;
using Responses;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain.ResponseValidations;
using AutoMapper;

namespace Application.Book.Update
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<BaseResponse<object>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            
            var bookDescription = new BookDescription(request.BookDescription.BookDescriptionId, request.BookDescription.AuthorName, request.BookDescription.PublicationName, request.BookDescription.EditionNumber);
            var bookDetails = new BookDetails(request.BookDetailsId, request.BookName, request.BookCode, request.RackId, request.RowId, Domain.DomainObjects.Enums.BookStatus.Available, null, null, request.LoggedUserName, DateTime.Today.Date, bookDescription);
            if (await _unitOfWork.Book.CheckUpdatedBookCodeExist(bookDetails))
            {
                var error = new List<ResponseValidation>() { new ResponseValidation("Book Code Already Exist") };
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            await _unitOfWork.Book.Update(bookDetails);
            return new BaseResponse<object>((object)null);
        }
    }


}
