using Entities.DataTransferObject;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContestRepository
    {
        Task<IEnumerable<HighlightContest>> getHightlightContest(bool trackChanges);
        Task<Pagination<ContestInGroup>> GetContestInGroup(int group_id, bool trackChanges, PagingParameters paging);
    }
}
