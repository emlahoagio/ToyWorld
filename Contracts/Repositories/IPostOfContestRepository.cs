using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPostOfContestRepository
    {
        void Create(PostOfContest postOfContest);
        Task<Pagination<PostOfContestInList>> GetPostOfContest(int contest_id, PagingParameters paging , bool trackChanges);
    }
}
