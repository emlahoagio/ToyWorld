using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IFollowGroupRepository
    {
        Task<IEnumerable<FollowGroup>> GetUserFollowGroup(int groupId);
        Task<bool> IsHasWishlist(int accountId, bool trackChanges);
    }
}
