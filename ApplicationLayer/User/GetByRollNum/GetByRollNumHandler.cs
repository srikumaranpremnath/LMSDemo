using Application.Book;
using Dapper;
using MediatR;
using Responses;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.User.GetByRollNum
{
    public class GetByRollNumHandler : IRequestHandler<GetByRollNumQuery, BaseResponse<List<UserDTO>>>
    {
        private readonly IDbConnection _dbConnection;
        public GetByRollNumHandler(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<BaseResponse<List<UserDTO>>> Handle(GetByRollNumQuery request, CancellationToken cancellationToken)
        {
            string query = @"SELECT Users.[UserDetailsId]
                              ,Users.[Name]
                              ,Users.[RollNum]
                              ,Users.[Department]
                              ,Users.[Password]
                              ,Users.[UserTypeId]
                              ,Users.[Email]
                              ,Users.[Mobile]
	                          ,Addresses.[AddressId]
                              ,Addresses.[HouseNo]
                              ,Addresses.[Street]
                              ,Addresses.[Area]
                              ,Addresses.[LandMark]
                              ,Addresses.[City]
                              ,Addresses.[State]
                              ,Addresses.[Country]
                              ,Addresses.[Pincode]
                        FROM [LMS].[dbo].[LMS.UserDetails] Users 
                             INNER JOIN [LMS.Address] Addresses 
	                         ON Users.UserDetailsId = Addresses.[UserDetailsId] 
	                         Where Users.RollNum=@rollNum AND UserTypeId=2 ";
            var user = await _dbConnection.QueryAsync(
                sql: query,
                new[]
                {
                    typeof(UserDTO),
                    typeof(AddressDTO),
                },
                (responseObj) =>
                {
                    var address = responseObj[1] as AddressDTO;
                    var user = responseObj[0] as UserDTO;
                    user.Address = address;
                    return user;
                },
                param: new { rollNum = request.RollNum },
                splitOn: "UserDetailsId,AddressId"
                );
            return new BaseResponse<List<UserDTO>>(user.ToList());
                
        }
    }
}
