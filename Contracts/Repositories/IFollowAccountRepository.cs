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
        void CreateFollow(FollowAccount followAccount);
        void DeleteFollow(FollowAccount followAccount);
    }
}
