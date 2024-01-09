using AutoMapper;
using Dapper;
using Domain;
using MediatR;
using Responses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Book.GetNotReturnedByUser
{
    public class GetNotReturnedByUserHandler : IRequestHandler<GetNotReturnedByUserQuery, BaseResponse<List<BookIssueDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDbConnection _dbconnection;
        public GetNotReturnedByUserHandler(IDbConnection dbConnection, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _dbconnection = dbConnection;
        }

        public async Task<BaseResponse<List<BookIssueDTO>>> Handle(GetNotReturnedByUserQuery request, CancellationToken cancellationToken)
        {
            Guid userId = await _unitOfWork.User.GetUserByRollNum(request.RollNum);
            string getUserQuery = @"SELECT [BookDetailsId]
                                          ,[IssuedDate]
                                          ,[ExpectedReturnDate]
                                          ,[RenewedDate]
                                          ,[ReturnedDate]
                                          ,[Penality]
                                          ,[PenalityStatus]
                                          ,[PenalityPaidDate]
                                          ,[StatusId]
                                      FROM [dbo].[LMS.BookIssued] where [UserDetailsId] = @userDetailsId and [StatusId]=2";
            var notReturnedBooks = await _dbconnection.QueryAsync<BookIssueDTO>(getUserQuery, new { userDetailsId = userId });
            await _unitOfWork.User.LoadPenality(request.RollNum);
            return new BaseResponse<List<BookIssueDTO>>(notReturnedBooks.ToList());
        }

    }
}
