using Entities.DataTransferObject;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IFollowGroupRepository
    {
        Task<IEnumerable<FollowGroup>> GetUserFollowGroup(int groupId);
        Task<bool> IsHasWishlist(int accountId, bool trackChanges);
        void Create(FollowGroup followGroup);
        Task<List<int>> GetFollowedGroup(int accountId, bool trackChanges);
        Task<AccountDetail> GetWishlist(AccountDetail account, bool trackChanges);
        void Delete(FollowGroup followGroup);
    }
}
