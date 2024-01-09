using Dapper;
using Domain.DomainObjects;
using Domain.Interfaces;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task Create(UserDetails userDetails)
        {
            string insertUserQuery = @"INSERT INTO [dbo].[LMS.UserDetails]
                                       ([UserDetailsId]
                                       ,[Name]
                                       ,[RollNum]
                                       ,[Department]
                                       ,[Password]
                                       ,[UserTypeId]
                                       ,[Email]
                                       ,[Mobile]
                                       ,[IsFirstLogin]
                                       ,[CreatedBy]
                                       ,[CreatedDate]
                                       )
                                 VALUES
                                       (@userDetailsId
                                       ,@name
                                       ,@rollNum
                                       ,@department
                                       ,@password
                                       ,@userTypeId
                                       ,@email
                                       ,@mobile
                                       ,@isFirstLogin
                                       ,@createdBy
                                       ,@createdDate
                                      )";
            string insertAddressQuery = @"INSERT INTO [dbo].[LMS.Address]
                                           ([AddressId]
                                           ,[UserDetailsId]
                                           ,[HouseNo]
                                           ,[Street]
                                           ,[Area]
                                           ,[LandMark]
                                           ,[City]
                                           ,[State]
                                           ,[Country]
                                           ,[Pincode]
                                           ,[CreatedBy]
                                           ,[CreatedDate])
                                     VALUES
                                           (@addressDetailsId
                                           ,@userdetailsId
                                           ,@houseNo
                                           ,@street
                                           ,@area
                                           ,@landMark
                                           ,@city
                                           ,@state
                                           ,@country
                                           ,@pincode
                                           ,@createdBy
                                           ,@createdDate)";
            await _dbConnection.ExecuteAsync(insertUserQuery, new
            {
                userDetailsId = userDetails.UserDetailsId,
                name = userDetails.Name,
                rollNum = userDetails.RollNum,
                department = userDetails.Department,
                password = userDetails.Password,
                userTypeId = userDetails.UserTypeId,
                email = userDetails.Email,
                mobile = userDetails.Mobile,
                isFirstLogin = userDetails.IsFirstLogin,
                createdBy = userDetails.CreatedBy,
                createdDate = userDetails.CreatedDate
            });
            await _dbConnection.ExecuteAsync(insertAddressQuery, new
            {
                addressDetailsId = userDetails.Address.AddressId,
                userDetailsId = userDetails.UserDetailsId,
                houseNo = userDetails.Address.HouseNo,
                street = userDetails.Address.Street,
                area = userDetails.Address.Area,
                landMark = userDetails.Address.LandMark,
                city = userDetails.Address.City,
                state = userDetails.Address.State,
                country = userDetails.Address.Country,
                pincode = userDetails.Address.Pincode,
                createdDate = userDetails.CreatedDate,
                createdBy = userDetails.CreatedBy
            });
        }
        public async Task Delete(UserDetails userDetails)
        {
            var deleteUserQuery = @"Update [LMS.UserDetails] 
                                    SET IsDeleted = 'true',
                                        EditedBy = @editedBy,
                                        EditedDate = @editedDate
                                    where UserDetailsId = @userDetailsId ";
            await _dbConnection.ExecuteAsync(deleteUserQuery, new 
                                            { 
                                                userDetailsId = userDetails.UserDetailsId,
                                                editedBy = userDetails.EditedBy,
                                                editedDate = userDetails.EditedDate
                                            });
        }
        public async Task ChangePassword(string newPassword, Guid userDetailsId)
        {
            string changePasswordQuery = @"Update [LMS.UserDetails] SET Password = @password WHERE [UserDetailsId]=@userDetailsId";
            await _dbConnection.ExecuteAsync(changePasswordQuery, new { password = newPassword, userDetailsId = userDetailsId });
        }
        public async Task<string> GetPassword(Guid userDetailsId)
        {
            string getPasswordQuery = @"SELECT Password from [LMS.UserDetails] where [UserDetailsId] = @userDetailsId";
            string password = await _dbConnection.ExecuteScalarAsync<string>(getPasswordQuery, new { userDetailsId = userDetailsId });
            return password;


        }
        public async Task<Guid> GetUserByRollNum(string rollNum)
        {
            string userByRollNumQuery = "Select UserDetailsId from [dbo].[LMS.UserDetails]  where RollNum = @rollNum ";
            Guid userDetialsId = await _dbConnection.ExecuteScalarAsync<Guid>(userByRollNumQuery, new { rollnum = rollNum });
            return userDetialsId;
        }
        public async Task<bool> CheckExistingRecords(Guid userDetailsId)
        {
            string bookCheckQuery = @"select 1 from [LMS.BookIssued] where UserDetailsId = @userDetailsId and [StatusId]=1";
            bool bookExist = await _dbConnection.ExecuteScalarAsync<bool>(bookCheckQuery, new { userDetailsId = userDetailsId });
            return (bookExist );
        }
        public async Task<UserDetails> Login(string username)
        {
            string loginQuery = @"Select 
                                       [UserDetailsId]
                                      ,[Name]
                                      ,[RollNum]
                                      ,[Password]
                                      ,[UserTypeId]
                                      ,[IsFirstLogin]
                                       from [dbo].[LMS.UserDetails] Where RollNum = @username ";
            var loggedUser = await _dbConnection.QueryFirstOrDefaultAsync<UserDetails>(loginQuery, new { username = username });
            return loggedUser;
        }
        public async Task Update(UserDetails userDetails)
        {
            string updateUserQuery = @" UPDATE [dbo].[LMS.UserDetails]
                                           SET [Name] = @name
                                              ,[Department] = @department
                                              ,[Email] = @email
                                              ,[Mobile] = @mobile
                                              ,[EditedBy] = @editedBy
                                              ,[EditedDate] = @editedDate
                                         WHERE [UserDetailsId] = @userDetailsId";

            string updateAddressQuery = @"UPDATE [dbo].[LMS.Address]
                                           SET [HouseNo] = @houseNo
                                              ,[Street] = @street
                                              ,[Area] = @area
                                              ,[LandMark] = @landMark
                                              ,[City] = @city
                                              ,[State] = @state
                                              ,[Country] = @country
                                              ,[Pincode] = @pincode
                                              ,[EditedBy] = @editedBy
                                              ,[EditedDate] = @editedDate
                                         WHERE [AddressId] = @addressDetailsId";
            await _dbConnection.ExecuteAsync(updateUserQuery, new
            {
                @userDetailsId = userDetails.UserDetailsId,
                @name = userDetails.Name,
                @department = userDetails.Department,
                @userTypeId = userDetails.UserTypeId,
                @email = userDetails.Email,
                @mobile = userDetails.Mobile,
                @editedBy = userDetails.EditedBy,
                @editedDate = userDetails.EditedDate

            });
            await _dbConnection.ExecuteAsync(updateAddressQuery, new
            {
                @addressDetailsId = userDetails.Address.AddressId,
                @houseNo = userDetails.Address.HouseNo,
                @street = userDetails.Address.Street,
                @area = userDetails.Address.Area,
                @landMark = userDetails.Address.LandMark,
                @city = userDetails.Address.City,
                @state = userDetails.Address.State,
                @country = userDetails.Address.Country,
                @pincode = userDetails.Address.Pincode,
                @editedBy = userDetails.EditedBy,
                @editedDate = userDetails.EditedDate

            });
        }
        public async Task<bool> CheckEmail(string emailId)
        {
            string checkEmailQuery = @"SELECT 1 from [LMS.UserDetails] WHERE [Email]=@email";
            return (await _dbConnection.ExecuteScalarAsync<bool>(checkEmailQuery, new { email = emailId }));
        }
        public async Task ResetPassword(UserDetails userDetails)
        {
            string resetPasswordQuery = @"UPDATE [LMS.UserDetails]  
                                         SET Password = @password,
                                            EditedBy=@editedBy,
                                            EditedDate=@editedDate
                                            WHERE [Email]=@email";
            await _dbConnection.ExecuteAsync(resetPasswordQuery, new
            {
                password = userDetails.Password,
                email = userDetails.Email,
                editedBy = userDetails.EditedBy,
                editedDate = userDetails.EditedDate
            });
            
        }
        public async Task<bool> CheckBatchDelete(string batchYear)
        {
            string checkBatchDeleteQuery = @" SELECT 1  FROM [LMS].[dbo].[LMS.BookIssued] 
                                              WHERE  UserDetailsId in 
                                                    (SELECT UserDetailsId FROM [LMS.UserDetails] 
                                                     WHERE RollNum Like @batchYear+'%') AND StatusId =2";
            return (await _dbConnection.ExecuteScalarAsync<bool>(checkBatchDeleteQuery, new { batchYear = batchYear }));
        }
        public async Task DeleteUserByBatch(string batchYear)
        {
            string batchDeleteQuery = @"Update [LMS].[dbo].[LMS.UserDetails] 
                                        SET IsDeleted ='true'
                                        WHERE RollNum Like @batchYear+'%')";
            await _dbConnection.ExecuteAsync(batchDeleteQuery, new { batchYear = batchYear });


        }

        public async Task<bool> CheckUpdatedMobile(UserDetails userDetails)
        {
            var checkMobileQuery = @"SELECT 1 FROM [LMS.UserDetails] WHERE Mobile = @mobile AND UserDetailsId = @userDetailsId";
            return (await _dbConnection.ExecuteScalarAsync<bool>(checkMobileQuery, new 
            {
                userDetailsId = userDetails.UserDetailsId,
                mobile = userDetails.Mobile
            }));
        }

        public async Task<bool> CheckUpdatedEmail(UserDetails userDetails)
        {
            var checkEmailQuery = @"SELECT 1 FROM [LMS.UserDetails] WHERE Email = @email AND UserDetailsId = @userDetailsId";
            return (await _dbConnection.ExecuteScalarAsync<bool>(checkEmailQuery, new
            {
                userDetailsId = userDetails.UserDetailsId,
                email = userDetails.Email
            }));
        }

        public async Task LoadPenality(string rollNum)
        {
            string loadPenalityQuery = @"UPDATE [LMS.BookIssued] SET 
                                        [Penality] = DATEDIFF(DAY,ExpectedReturnDate,GETDATE())
						                            ,PenalityStatusId=2 
						                            WHERE GETDATE()>ExpectedReturnDate AND UserDetailsId IN 
						                            (SELECT UserDetailsId FROM [LMS.UserDetails] WHERE RollNum = @rollNum)";
            await _dbConnection.ExecuteAsync(loadPenalityQuery,new { rollNum= rollNum});
        }
    }
}
