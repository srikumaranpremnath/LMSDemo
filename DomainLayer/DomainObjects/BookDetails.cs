using Domain.DomainObjects.Enums;
using Domain.Interfaces;
using Domain.ResponseValidations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DomainObjects
{
    public class BookDetails : Audit
    {
        public BookDetails(Guid? bookDetailsId, string bookName, string bookCode,int? rackId, int? rowId,BookStatus bookStatusId,
              string createdBy, DateTime? createdDate, string editedby, DateTime? editedDate,
                #nullable enable
              BookDescription? bookDesccription)
        {
            BookDetailsId = bookDetailsId;
            BookName = bookName;
            BookCode=bookCode;
            RackId = rackId;
            RowId = rowId;
            BookStatusId = bookStatusId;
            CreatedBy = createdBy;
            EditedBy = editedby;
            CreatedDate = createdDate;
            EditedDate = editedDate;
            BookDescription = bookDesccription;

        }
        public Guid? BookDetailsId { get; private set; }
        public string BookCode { get; private set; }
        public string BookName { get; private set; }
        public int? RackId { get; private set; }
        public int? RowId { get; private set; }
        public BookStatus? BookStatusId { get; set; }
        
        #nullable enable
        public BookDescription? BookDescription { get; private set; }

     
    }
    public class BookDescription 
    {
        public BookDescription() { }
        public BookDescription(Guid? bookDescriptionId, string authorName, string publicationName, int? editionNumber)
        {
            BookDescriptionId = bookDescriptionId;
            AuthorName = authorName;
            PublicationName = publicationName;
            EditionNumber = editionNumber;
        }
        public Guid? BookDescriptionId { get; set; }
        public string AuthorName { get; private set; }
        public string PublicationName { get; private set; }
        public int? EditionNumber { get; private set; }
    }


}

