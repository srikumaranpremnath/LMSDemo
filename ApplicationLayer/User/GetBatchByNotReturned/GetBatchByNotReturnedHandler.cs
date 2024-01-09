using AutoMapper;
using Dapper;
using MediatR;
using Responses;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.GetBatchByNotReturned
{
    public class GetBatchByNotReturnedHandler : IRequestHandler<GetBatchByNotReturnedQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly IDbConnection _dbconnection;
        public GetBatchByNotReturnedHandler(IDbConnection dbConnection)
        {
            _dbconnection = dbConnection;
        }
        public async Task<BaseResponse<List<UserDTO>>> Handle(GetBatchByNotReturnedQuery request, CancellationToken cancellationToken)
        {
            string getNotReturnedUsersQuery = @"SELECT Users.[UserDetailsId]
                                                  ,Users.[Name]
                                                  ,Users.[RollNum]
                                                  ,Users.[Department]
                                                  ,Users.[Email]
                                                  ,Users.[Mobile]
                                            FROM [LMS.UserDetails] as Users
                                            INNER JOIN [LMS.BookIssued] as Book
                                            ON Users.UserDetailsId = Book.UserDetailsId WHERE 
                                            Book.StatusId=2 and Users.RollNum like  @batchYear+'%'";
            var notReturnedUsers = await _dbconnection.QueryAsync<UserDTO>(getNotReturnedUsersQuery, new { batchYear = request.batchYear });
            return new BaseResponse<List<UserDTO>>(notReturnedUsers.ToList());
        }
    }
}
