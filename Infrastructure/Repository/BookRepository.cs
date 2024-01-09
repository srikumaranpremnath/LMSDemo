using Domain.DomainObjects;
using Domain.Interfaces;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Domain.DomainObjects.Enums;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;

namespace Infrastructure.Repository
{
    public class BookRepository : ProcedureList, IBookRepository
    {
        private readonly IDbConnection _dbConnection;
        public BookRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        //        public async Task Create(BookDetails bookDetails)
        //        {
        //            string insertBookQuery = @"INSERT INTO [dbo].[LMS.BookDetails]
        //                                       ([BookDetailsId]
        //                                       ,[BookCode]
        //                                       ,[BookName]
        //                                       ,[RackId]
        //                                       ,[RowId]
        //                                       ,[StatusId] 
        //                                       ,[CreatedBy]
        //                                       ,[CreatedDate])
        //                                VALUES
        //                                       (@bookDetailsId
        //                                       ,@bookCode
        //                                       ,@bookName
        //                                       ,@rackId
        //                                       ,@rowId
        //                                       ,@statusId
        //                                       ,@createdBy
        //                                       ,@createdDate)";
        //            string insertBookDescriptionQuery = @"INSERT INTO [dbo].[LMS.BookDescription]
        //                                                               ([BookDescriptionId]
        //                                                               ,[BookDetailsId]
        //                                                               ,[AuthorName]
        //                                                               ,[PublicationsName]
        //                                                               ,[EditionNumber]
        //                                                               ,[CreatedBy]
        //                                                               ,[CreatedDate])
        //                                                  VALUES
        //                                                        (@bookDescriptionId
        //                                                        , @bookDetailsId
        //                                                        , @authorName
        //                                                        , @publicationsName
        //                                                        , @editionNumber
        //                                                        , @createdBy
        //                                                        , @createdDate)"
        //;

        //            await _dbConnection.ExecuteAsync(insertBookQuery, new
        //            {
        //                bookDetailsId = bookDetails.BookDetailsId,
        //                bookCode = bookDetails.BookCode,
        //                bookName = bookDetails.BookName,
        //                rackId = bookDetails.RackId,
        //                rowId = bookDetails.RowId,
        //                statusId = bookDetails.BookStatusId,
        //                createdBy = bookDetails.CreatedBy,
        //                createdDate = bookDetails.CreatedDate
        //            });
        //            await _dbConnection.ExecuteAsync(insertBookDescriptionQuery, new
        //            {
        //                bookDescriptionId = bookDetails.BookDescription.BookDescriptionId,
        //                bookDetailsId = bookDetails.BookDetailsId,
        //                authorName = bookDetails.BookDescription.AuthorName,
        //                publicationsName = bookDetails.BookDescription.PublicationName,
        //                editionNumber = bookDetails.BookDescription.EditionNumber,
        //                createdBy = bookDetails.CreatedBy,
        //                createdDate = bookDetails.CreatedDate
        //            });

        //        }

        public async Task Create(BookDetails bookDetails)
        {
            var parameters = new
            {
                bookDetailsId = bookDetails.BookDetailsId,
                bookCode = bookDetails.BookCode,
                bookName = bookDetails.BookName,
                rackId = bookDetails.RackId,
                rowId = bookDetails.RowId,
                statusId = bookDetails.BookStatusId,
                createdBy = bookDetails.CreatedBy,
                createdDate = bookDetails.CreatedDate,
                bookDescriptionId = bookDetails.BookDescription.BookDescriptionId,
                authorName = bookDetails.BookDescription.AuthorName,
                publicationsName = bookDetails.BookDescription.PublicationName,
                editionNumber = bookDetails.BookDescription.EditionNumber,

            };

            await _dbConnection.ExecuteAsync(CreateBook_Procedure, parameters, commandType: CommandType.StoredProcedure);
        }
        public async Task Update(BookDetails bookDetails)
        {
            string updateBookQuery = @"UPDATE [dbo].[LMS.BookDetails]
                                       SET [BookCode] = @bookCode
                                          ,[BookName] = @bookName
                                          ,[RackId] = @rackId
                                          ,[RowId] = @rowId
                                          ,[StatusId] = @statusId
                                          ,[EditedBy] = @editedBy
                                          ,[EditedDate] = @editedDate
                                     WHERE [BookDetailsId]=@bookDetailsId";
            string updateBookDescriptionQuery = @"UPDATE [dbo].[LMS.BookDescription]
                                                  SET [AuthorName] = @authorName
                                                      ,[PublicationsName] = @publicationName
                                                      ,[EditionNumber] = @editionNumber
                                                      ,[EditedBy] = @editedBy
                                                      ,[EditedDate] = @editedDate
                                                 WHERE [BookDescriptionId] = @bookDescriptionId
                                                ";
            await _dbConnection.ExecuteAsync(updateBookQuery, new
            {
                bookDetailsId = bookDetails.BookDetailsId,
                bookCode = bookDetails.BookCode,
                bookName = bookDetails.BookName,
                rackId = bookDetails.RackId,
                rowId = bookDetails.RowId,
                statusId = bookDetails.BookStatusId,
                editedBy = bookDetails.EditedBy,
                editedDate = bookDetails.EditedDate

            });
            await _dbConnection.ExecuteAsync(updateBookDescriptionQuery, new
            {
                bookDescriptionId = bookDetails.BookDescription.BookDescriptionId,
                authorName = bookDetails.BookDescription.AuthorName,
                publicationName = bookDetails.BookDescription.PublicationName,
                editionNumber = bookDetails.BookDescription.EditionNumber,
                editedBy = bookDetails.EditedBy,
                editedDate = bookDetails.EditedDate
            });


        }
        public async Task IssueBook(BookIssue issueBook)
        {
            string issueBookQuery = @"INSERT INTO [dbo].[LMS.BookIssued]
                                       ([BookIssuedId]
                                       ,[BookDetailsId]
                                       ,[UserDetailsId]
                                       ,[IssuedDate]
                                       ,[ExpectedReturnDate]
                                       ,[StatusId]
                                       ,[CreatedBy]
                                       ,[CreatedDate]
                                      )
                                         VALUES
                                               (@bookIssueId,
                                               @bookDetailsId,
                                                @userDetailsId,
                                                @issuedDate,
                                                @expectedReturnDate,
                                                @statusId, 
                                                @createdBy,
                                                @createdDate)";
            string bookStatusQuery = @"UPDATE [dbo].[LMS.BookDetails]
                                       SET [StatusId] = @statusId,
                                           [EditedBy] = @editedBy,
                                          [EditedDate] = @editedDate
                                     WHERE [BookDetailsId] = @bookDetailsId";


            await _dbConnection.ExecuteAsync(issueBookQuery,
                  new
                  {
                      bookIssueId = issueBook.BookIssueId,
                      bookDetailsId = issueBook.BookDetailsId,
                      userDetailsId = issueBook.UserDetailsId,
                      issuedDate = issueBook.IssuedDate,
                      expectedReturnDate = issueBook.ExpectedReturnDate,
                      statusId = issueBook.ReturnedStatusId,
                      createdBy = issueBook.CreatedBy,
                      createdDate = issueBook.CreatedDate
                  });

            await _dbConnection.ExecuteAsync(bookStatusQuery,
                  new
                  {
                      bookDetailsId = issueBook.BookDetailsId,
                      statusId = BookStatus.Unavailable,
                      editedBy = issueBook.CreatedBy,
                      editedDate = issueBook.CreatedDate
                  });

        }
        public async Task ReturnBook(BookIssue returnBook)
        {
            string returnQuery = @"UPDATE [dbo].[LMS.BookIssued]
                                    SET 
                                        ReturnedDate = @returnedDate,
                                        EditedBy = @editedBy,
                                        EditedDate = @editedDate,
                                        PenalityStatusId = @penalityStatusId,
                                        StatusId = @statusId
                                    WHERE 
                                        BookDetailsId = @bookDetailsId
                                       ";
            string bookStatusQuery = @"UPDATE [dbo].[LMS.BookDetails]
                                       SET [StatusId] = @statusId,
                                           [EditedBy] = @editedBy,
                                          [EditedDate] = @editedDate
                                    WHERE 
                                        BookDetailsId = @bookDetailsId";
            await _dbConnection.ExecuteAsync(returnQuery,
                 new
                 {
                     bookDetailsId = returnBook.BookDetailsId,
                     returnedDate = returnBook.ReturnedDate,
                     editedBy = returnBook.EditedBy,
                     editedDate = returnBook.EditedDate,
                     penalityStatusId = returnBook.PenalityStatusId,
                     statusId = returnBook.ReturnedStatusId
                 });
            await _dbConnection.ExecuteAsync(bookStatusQuery,
                   new
                   {
                       bookDetailsId = returnBook.BookDetailsId,
                       statusId = BookStatus.Available,
                       editedBy = returnBook.EditedBy,
                       editedDate = returnBook.EditedDate
                   });
        }
        public async Task RenewBook(BookIssue renewBook)
        {

            string renewQuery = @" UPDATE [dbo].[LMS.BookIssued]
                                    SET 
                                        ExpectedReturnDate = @expectedReturndate,
                                        RenewedDate = @renewedDate,
                                        EditedBy = @editedBy,
                                        EditedDate = @editedDate

                                    WHERE 
                                        BookDetailsId = @bookDetailsId";


            await _dbConnection.ExecuteAsync(renewQuery,
                 new
                 {
                     bookDetailsId = renewBook.BookDetailsId,
                     expectedReturnDate = renewBook.ExpectedReturnDate,
                     renewedDate = renewBook.RenewedDate,
                     editedBy = renewBook.EditedBy,
                     editedDate = renewBook.EditedDate
                 });

        }
        public async Task<bool> CheckBookCodeExist(string bookCode)
        {
            string checkQuery = @"Select 1 from dbo.[LMS.BookDetails] where BookCode = @bookCode";
            return (await _dbConnection.ExecuteScalarAsync<bool>(checkQuery, new { bookCode = bookCode }));
        }
        public async Task<int> BookAvailabilityCheck(string bookCode)
        {
            string availabiltyQuery = @"select StatusId from[LMS.BookDetails] WHERE
                                      BookDetailsId =
                                      (
                                          select BookDetailsId from[LMS.BookDetails]
                                            where BookCode = @bookCode
                                        ) ";
            return await _dbConnection.ExecuteScalarAsync<int>(availabiltyQuery, new { bookCode = bookCode });
        }
        public async Task<Guid> GetBookByBookCode(string bookCode)
        {
            string bookByCodeQuery = "Select BookDetailsId from [dbo].[LMS.BookDetails]  where BookCode = @BookCode ";
            Guid bookDetailsId = await _dbConnection.ExecuteScalarAsync<Guid>(bookByCodeQuery, new { bookCode = bookCode });
            return bookDetailsId;
        }
        public async Task<Guid> GetBookIssueByBookDetailsId(Guid bookDetailsId)
        {
            string getBookIssueDetailsByBookDetailsIdQuery = "Select BookIssuedId from  [dbo].[LMS.BookIssued]  where BookDetailsId = @bookDetailsId ";
            Guid bookIssuedId = await _dbConnection.ExecuteScalarAsync<Guid>(getBookIssueDetailsByBookDetailsIdQuery, new { bookDetailsId = bookDetailsId });
            return bookIssuedId;
        }
        public async Task<DateTime> GetExpectedReturnDate(Guid bookIssuedId)
        {
            string expectedReturnDateQuery = "Select ExpectedReturndate from  [dbo].[LMS.BookIssued] where BookIssuedId = @bookIssuedId ";
            DateTime expectedReturnDate = await _dbConnection.ExecuteScalarAsync<DateTime>(expectedReturnDateQuery, new { bookIssuedId = bookIssuedId });
            return expectedReturnDate;
        }
        public async Task Delete(BookDetails bookDetails)
        {
            string deleteBookQuery = @"Update [LMS.BookDetails] 
                                              SET IsBookDeleted ='true', 
                                                  BookStatusId = @bookStatusId,
                                                  EditedBy = @editedBy,
                                                  EditedDate = @editedDate
                                                  where BookCode = @bookCode";
            await _dbConnection.ExecuteAsync(deleteBookQuery, new
            {
                bookCode = bookDetails.BookCode,
                bookStatusId = bookDetails.BookStatusId,
                editedBy = bookDetails.EditedBy,
                editedDate = bookDetails.EditedDate,
            });
        }
        public async Task<bool> HasExistingRecords(Guid bookDetailsId)
        {
            string bookCheckQuery = @"select 1 from[LMS.BookDetails] where BookDetailsId = @bookDetailsId";
            bool bookExist = await _dbConnection.ExecuteScalarAsync<bool>(bookCheckQuery, new { bookDetailsId = bookDetailsId });
            return bookExist;
        }
        public async Task<bool> CheckUpdatedBookCodeExist(BookDetails bookDetails)
        {
            string checkBookCodeQuery = @"SELECT 1 FROM [LMS.BookDetails] WHERE BookCode = @bookCode and BookDetailsId != @bookDetailsId";
            return (await _dbConnection.ExecuteScalarAsync<bool>(checkBookCodeQuery, new
            {
                bookCode = bookDetails.BookCode,
                bookDetailsId = bookDetails.BookDetailsId
            }));

        }
    }

}
