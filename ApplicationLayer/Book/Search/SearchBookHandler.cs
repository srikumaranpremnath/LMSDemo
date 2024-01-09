using Dapper;
using MediatR;
using Responses;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Book.SearchBook
{
    public class SearchBookHandler : IRequestHandler<SearchBookQuery, BaseResponse<List<BookDTO>>>
    {
        private readonly IDbConnection _dbconnection;
        public SearchBookHandler(IDbConnection dbConnection)
        {
            _dbconnection = dbConnection;
        }

        public async Task<BaseResponse<List<BookDTO>>> Handle(SearchBookQuery request, CancellationToken cancellationToken)
        {
            string searchQuery = @"SELECT 
                                      B.BookDetailsId,
                                      B.BookCode,
                                      B.BookName,
                                      B.RackId,
                                      B.RowId,
                                      B.StatusId AS BookStatus,
                                      BD.BookDescriptionId,
                                      BD.AuthorName,
                                      BD.PublicationsName AS PublicationName,
                                      BD.EditionNumber
                                FROM [LMS].[dbo].[LMS.BookDetails] B 
								     INNER JOIN [LMS.BookDescription] BD 
                                     ON
                                     BD.BookDetailsId=B.BookDetailsId 
									 WHERE 
                                            B.BookCode like @keyword+'%' or 
                                            B.BookName like @keyword+'%' or 
                                            BD.AuthorName like @keyword+'%' or 
                                            BD.PublicationsName like @keyword+'%'";
            var searchedBooks = await _dbconnection.QueryAsync(
                 sql: searchQuery,
                 new[]
                 {
                     typeof(BookDTO),
                     typeof(BookDescriptionDTO)
                 },
                 (responseObj) =>
                 {
                     var bookDescription = responseObj[1] as BookDescriptionDTO;
                     var book = responseObj[0] as BookDTO;
                     book.BookDescription = bookDescription;
                     return book;
                 },
                 param: new { keyword = request.Keyword},
                 splitOn: "BookDetailsId,BookDescriptionId"
                 );

                
            return new BaseResponse<List<BookDTO>>(searchedBooks.ToList());
        }

    }
}
