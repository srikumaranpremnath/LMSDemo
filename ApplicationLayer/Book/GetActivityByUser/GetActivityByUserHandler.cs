using Application.Book.GetNotReturnedByUser;
using Dapper;
using Domain;
using MediatR;
using Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Book.GetActivityByUser
{
    public class GetActivityByUserHandler : IRequestHandler<GetActivityByUserQuery, BaseResponse<List<BookIssueDTO>>>
    {
        private readonly IDbConnection _dbconnection;
        private readonly IUnitOfWork _unitOfWork;
        public GetActivityByUserHandler(IDbConnection dbConnection,IUnitOfWork unitOfWork)
        {
            _dbconnection = dbConnection;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponse<List<BookIssueDTO>>> Handle(GetActivityByUserQuery request, CancellationToken cancellationToken)
        {
            Guid userDetailsId = await _unitOfWork.User.GetUserByRollNum(request.RollNum);
            string getUserQuery = @"SELECT [BookDetailsId]
                                          ,[IssuedDate]
                                          ,[ExpectedReturnDate]
                                          ,[RenewedDate]
                                          ,[ReturnedDate]
                                          ,[Penality]
                                          ,[PenalityStatusId]
                                          ,[PenalityPaidDate]
                                          ,[StatusId]
                                      FROM [dbo].[LMS.BookIssued] where [UserDetailsId] = @userDetailsId";
            var notReturnedBooks = await _dbconnection.QueryAsync<BookIssueDTO>(getUserQuery, new { userDetailsId = userDetailsId });
            return new BaseResponse<List<BookIssueDTO>>(notReturnedBooks.ToList());
        }
    }
}
