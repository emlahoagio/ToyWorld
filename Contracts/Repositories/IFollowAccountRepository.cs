using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IFollowAccountRepository
    {
        Task<FollowAccount> GetFollowAccount(FollowAccount followAccount, bool trackChanges);
        Task<List<AccountReact>> GetAccountFollowing(int account_id, bool trackChanges);
        Task<List<AccountReact>> GetAccountFollower(int account_id, bool trackChanges);
        void CreateFollow(FollowAccount followAccount);
        void DeleteFollow(FollowAccount followAccount);
    }
}
