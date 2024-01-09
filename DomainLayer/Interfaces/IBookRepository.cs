using Domain.DomainObjects;
using DomainCommon;
using System;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBookRepository : IGenericRepository<BookDetails>
    {
        public Task IssueBook(BookIssue IssueBook);
        public Task RenewBook(BookIssue renewBook);
        public Task ReturnBook(BookIssue returnBook);
        public Task<int> BookAvailabilityCheck(string bookCode);
        public Task<Guid> GetBookByBookCode(string bookCode);
        public Task<Guid> GetBookIssueByBookDetailsId(Guid bookDetailsId);
        public Task<DateTime> GetExpectedReturnDate(Guid bookIssueId);
        public Task<bool> CheckBookCodeExist(string bookCode);
        public Task<bool> HasExistingRecords(Guid bookDetailsId);
        public Task<bool> CheckUpdatedBookCodeExist(BookDetails bookDetails);


    }
}
