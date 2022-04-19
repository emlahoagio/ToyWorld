using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class FollowGroupRepository : RepositoryBase<FollowGroup>, IFollowGroupRepository
    {
        public FollowGroupRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<int>> GetFollowedGroup(int accountId, bool trackChanges)
        {
            var result = await FindByCondition(x => x.AccountId == accountId, trackChanges).Select(x => x.GroupId).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<FollowGroup>> GetUserFollowGroup(int groupId)
        {
            var users = await FindByCondition(x => x.GroupId == groupId, false).ToListAsync();
            return users;
        }

        public async Task<AccountDetail> GetWishlist(AccountDetail account, bool trackChanges)
        {
            var followedGroup = await FindByCondition(x => x.AccountId == account.Id, trackChanges)
                .Include(x => x.Group)
                .ToListAsync();

            account.WishLists = followedGroup.Select(x => new WishList { Id = x.Group.Id, Name = x.Group.Name }).ToList();

            return account;
        }

        public async Task<bool> IsHasWishlist(int accountId, bool trackChanges)
        {
            var wishList = await FindByCondition(x => x.AccountId == accountId, trackChanges).FirstOrDefaultAsync();

            return wishList != null;
        }
    }
}
