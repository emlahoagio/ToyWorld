using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IGroupRepository
    {
        Task<IEnumerable<string>> getListGroup(bool trackChanges);
    }
}
