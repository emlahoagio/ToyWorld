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
    }
}
