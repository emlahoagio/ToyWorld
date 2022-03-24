using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGroupRepository
    {
        Task<List<GroupReturn>> getListGroup(bool trackChanges);
        Task Update(int group_id, string name, string description, bool trackChanges);
        Task DisableOrEnableGroup(int group_id, int disable_or_enable, bool trackChanges);
        void Create(Entities.Models.Group group);
    }
}
