using Domain.DomainObjects.Enums;
using Domain.ResponseValidations;
using System;
using System.Collections.Generic;

namespace Domain.DomainObjects
{
    public class BookIssue : Audit
    {
        public BookIssue(Guid bookIssueId, Guid? userDetailsId, Guid? bookDetailsId, DateTime? issuedDate,
            DateTime? expectedReturnDate, DateTime? returnedDate, DateTime? renewedDate, string bookCode,
            int? penality, DateTime? penalityPaidDate, PenalityStatus? penalityStatusId,ReturnedStatus? returnedStatusId,
            string createdBy, DateTime? createdDate, string editedby, DateTime? editedDate)
        {
            BookIssueId = bookIssueId;
            UserDetailsId = userDetailsId;
            BookDetailsId = bookDetailsId;
            IssuedDate = issuedDate;
            ExpectedReturnDate = expectedReturnDate;
            ReturnedDate = returnedDate;
            RenewedDate = renewedDate;
            BookCode = bookCode;
            Penality = penality;
            PenalityPaidDate = penalityPaidDate;
            PenalityStatusId = penalityStatusId;
            CreatedBy = createdBy;
            EditedBy = editedby;
            ReturnedStatusId = returnedStatusId;
            CreatedDate = createdDate;
            EditedDate = editedDate;

        }
        public Guid BookIssueId { get; private set; }

        public Guid? UserDetailsId { get; private set; }

        public Guid? BookDetailsId { get; private set; }
        public string BookCode { get; private set; }

        public DateTime? IssuedDate { get; private set; }
        public DateTime? ExpectedReturnDate { get; private set; }

        public DateTime? ReturnedDate { get; private set; }

        public DateTime? RenewedDate { get; private set; }
        public PenalityStatus? PenalityStatusId { get; private set; }
        public int? Penality { get; private set; }
        public DateTime? PenalityPaidDate { get; private set; }
        public ReturnedStatus? ReturnedStatusId { get; private set; }
        public IReadOnlyList<ResponseValidation> CanIssue(int StatusId)
        {
            var error = new List<ResponseValidation>();
            if (Convert.ToInt32(BookStatus.Available) != StatusId)
            {
                error.Add(new ResponseValidation("Book already Taken"));
            }
            return error;

        }
        public IReadOnlyList<ResponseValidation> CanRenew(DateTime ExpectedReturnDate)
        {
            var error = new List<ResponseValidation>();
            if (DateTime.Today > ExpectedReturnDate)
            {
                error.Add(new ResponseValidation("Renew Date passed, Please return the book"));
            }
            return error;

        }
    }
}
