using MediatR;
using Responses;
using System;

namespace Application.Book.Update
{
        public class UpdateBookCommand : IRequest<BaseResponse<object>>
        {
            public Guid BookDetailsId { get; set; }
            public string BookName { get; set; }
            public int RackId { get; set; }
            public int RowId { get; set; }
            public string BookCode { get; set; }
            public UpdateBookDescriptionCommand BookDescription { get; set; }
            public string LoggedUserName { get; set; }


    }
        public class UpdateBookDescriptionCommand
        {
            public Guid BookDescriptionId { get; set; }
            public string AuthorName { get; set; }
            public string PublicationName { get; set; }
            public int? EditionNumber { get; set; }
        }


}
