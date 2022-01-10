using Contracts;
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
        public async Task<IEnumerable<string>> getListGroup(bool trackChanges)
        {
            var groupList = await FindAll(trackChanges).OrderBy(x => x.Name).ToListAsync();

            if (groupList == null) return null;

            List<string> result = new List<string>();

            foreach(Group group in groupList)
            {
                result.Add(group.Name);
            }

            return result;
        }
    }
}
