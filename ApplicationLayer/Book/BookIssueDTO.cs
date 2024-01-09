using Domain.DomainObjects.Enums;
using System;

namespace Application.Book
{
    public class BookIssueDTO
    {
        public Guid BookIssueId { get; private set; }
        public Guid? BookDetailsId { get; private set; }
        public string BookCode { get; private set; }
        public DateTime? IssuedDate { get; private set; }
        public DateTime? ExpectedReturnDate { get; private set; }
        public DateTime? ReturnedDate { get; private set; }
        public DateTime? RenewedDate { get; private set; }
        public PenalityStatus? PenalityStatusId { get; private set; }
        public int? Penality { get; private set; }
        public DateTime? PenalityPaidDate { get; private set; }

    }
}
