using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FollowGroupRepository : RepositoryBase<FollowGroup>, IFollowGroupRepository
    {
        public FollowGroupRepository(DataContext context) : base(context)
        {

        }
        public async Task<IEnumerable<FollowGroup>> GetUserFollowGroup(int groupId)
        {
            var users = await FindByCondition(x => x.GroupId == groupId, false).ToListAsync();
            return users;
        }

        public async Task<bool> IsHasWishlist(int accountId, bool trackChanges)
        {
            var wishList = await FindByCondition(x => x.AccountId == accountId, trackChanges).FirstOrDefaultAsync();

            return wishList != null;
        }
    }
}
