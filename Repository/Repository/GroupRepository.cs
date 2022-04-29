using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(DataContext context) : base(context)
        {

        }

        public async Task DisableOrEnableGroup(int group_id, int disable_or_enable, bool trackChanges)
        {
            var group = await FindByCondition(x => x.Id == group_id, trackChanges).FirstOrDefaultAsync();

            if (group == null) return;

            if (disable_or_enable == 1) group.IsDisable = false;
            else group.IsDisable = true;

            Update(group);
        }

        public async Task<List<GroupReturn>> GetListGroup(bool trackChanges)
        {
            var groupList = await FindByCondition(x => x.IsDisable == false, trackChanges).OrderBy(x => x.Name).ToListAsync();

            if (groupList == null) return null;

            var result = groupList.Select(x => new GroupReturn
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CoverImage = x.CoverImage
            }).ToList();

            return result;
        }

        public async Task Update(int group_id, string name, string description, bool trackChanges)
        {
            var group = await FindByCondition(x => x.Id == group_id, trackChanges).FirstOrDefaultAsync();

            group.Name = name;
            group.Description = description;

            Update(group);
        }
    }
}
