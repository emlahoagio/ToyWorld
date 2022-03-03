using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JoinedContestRepository : RepositoryBase<JoinedToContest>, IJoinedContestRepository
    {
        public JoinedContestRepository(DataContext context) : base(context)
        {
        }

        public async Task<Pagination<AccountInList>> GetListSubscribers(int contest_id, PagingParameters paging, bool trackChanges)
        {
            var accounts = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Include(x => x.Account)
                .OrderBy(x => x.Account.Name)
                .ToListAsync();

            var count = accounts.Count;

            var paging_accounts = accounts.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var account_list = paging_accounts.Select(x => new AccountInList
            {
                Avatar = x.Account.Avatar,
                Id = x.Account.Id,
                Name = x.Account.Name,
                Phone = x.Account.Phone,
                Status = x.Account.Status.ToString()
            }).ToList();

            var result = new Pagination<AccountInList>
            {
                Count = count,
                Data = account_list,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public async Task<bool> IsJoinedToContest(int contest_id, int account_id, bool trackChanges)
        {
            var joinedContest = await FindByCondition(x => x.ContestId == contest_id && x.AccountId == account_id, trackChanges)
                .FirstOrDefaultAsync();

            return joinedContest == null ? false : true;
        }
    }
}
