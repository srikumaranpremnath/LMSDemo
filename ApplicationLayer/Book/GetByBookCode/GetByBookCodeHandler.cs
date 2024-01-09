using Dapper;
using MediatR;
using Responses;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Book.GetByBookCode
{
    public class GetByBookCodeHandler : IRequestHandler<GetByBookCodeQuery, BaseResponse<List<BookDTO>>>
    {
        private readonly IDbConnection _dbConnection;
        public GetByBookCodeHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<BaseResponse<List<BookDTO>>> Handle(GetByBookCodeQuery request, CancellationToken cancellationToken)
        {
            string query = @"SELECT Book.[BookDetailsId]
                              ,Book.[BookCode]
                              ,Book.[BookName]
                              ,Book.[RackId]
                              ,Book.[RowId]
                              ,Book.[StatusId]
                              ,BD.[BookDescriptionId]
                              ,BD.[BookDetailsId]
                              ,BD.[AuthorName]
                              ,BD.[PublicationsName]
                              ,BD.[EditionNumber]
                          FROM
                          [LMS.BookDetails] Book  INNER JOIN  [LMS.BookDescription] BD
                          ON Book.BookDetailsId= BD.BookDetailsId
                          Where BookCode = @bookCode";
            var book = await _dbConnection.QueryAsync(
                sql:query,
                new[]
                {
                    typeof(BookDTO),
                    typeof(BookDescriptionDTO)
                },
                (responseObj)=>
                {
                    var bookDescription = responseObj[1] as BookDescriptionDTO;
                    var book = responseObj[0] as BookDTO;
                    book.BookDescription = bookDescription;
                    return book;
                },
                param : new {bookCode = request.BookCode},
                splitOn :"BookDetailsId,BookDescriptionId"
                );
            return new BaseResponse<List<BookDTO>>(book.ToList());
        }
    }
}
