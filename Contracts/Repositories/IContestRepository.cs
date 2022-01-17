using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IContestRepository
    {
        Task<IEnumerable<HighlightContest>> getHightlightContest(bool trackChanges);
    }
}
