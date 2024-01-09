using Application.Book.RenewBook;
using AutoMapper;
using Domain.DomainObjects;
using Domain.ResponseValidations;
using Responses;

namespace Application.Book
{
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookIssue, RenewBookDTO>();
            CreateMap<ResponseValidation, ResponseErrors>();
            CreateMap<BookDetails, BookDTO>();
        }
    }
}
