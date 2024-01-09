using AutoMapper;
using Domain;
using Domain.DomainObjects;
using Domain.ResponseValidations;
using MediatR;
using Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Book.CreateBook
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, BaseResponse<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateBookHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<BaseResponse<object>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            Guid bookDetailsId = Guid.NewGuid();
            Guid bookDescriptionId = Guid.NewGuid();
            bool isBookCodeExist = await _unitOfWork.Book.CheckBookCodeExist(request.BookCode);
            if (isBookCodeExist)
            {
                var error = new List<ResponseValidation>
                {
                    new ResponseValidation("Book Code Already Exist")
                };
                return new BaseResponse<object>(_mapper.Map<List<ResponseErrors>>(error));
            }
            var bookDescription = new BookDescription(bookDescriptionId, request.BookDescription.AuthorName, request.BookDescription.PublicationName, request.BookDescription.EditionNumber);
            var bookDetails = new BookDetails(bookDetailsId,request.BookName,request.BookCode,request.RackId,request.RowId,Domain.DomainObjects.Enums.BookStatus.Available,request.LoggedUserName,DateTime.Today.Date,null,null,bookDescription);
            await _unitOfWork.Book.Create(bookDetails);
            return new BaseResponse<object>((object)null);


        }
    }
}
