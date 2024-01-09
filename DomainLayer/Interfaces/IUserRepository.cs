using Domain.DomainObjects;
using DomainCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserDetails>
    {
        public Task<Guid> GetUserByRollNum(string rollNum);
        public Task<UserDetails> Login(string username);
        public Task<bool> CheckExistingRecords(Guid userDetailsId);
        public Task<string> GetPassword(Guid userDetailsId);
        public Task ChangePassword(string newPassword,Guid userDetailsId);
        public Task<bool> CheckEmail(string emailId);

        public Task ResetPassword(UserDetails userDetails);

        public Task<bool> CheckBatchDelete(string BatchYear);

        public Task DeleteUserByBatch(string BatchYear);

        public Task<bool> CheckUpdatedMobile(UserDetails userDetails);

        public Task<bool> CheckUpdatedEmail(UserDetails userDetails);

        public Task LoadPenality(string rollNum);

    }
}
