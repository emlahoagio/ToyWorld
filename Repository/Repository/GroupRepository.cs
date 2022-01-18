using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(DataContext context) : base(context)
        {

        }
        public async Task<List<GroupReturn>> getListGroup(bool trackChanges)
        {
            var groupList = await FindAll(trackChanges).OrderBy(x => x.Name).ToListAsync();

            if (groupList == null) return null;

            var result = groupList.Select(x => new GroupReturn
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return result;
        }
    }
}
