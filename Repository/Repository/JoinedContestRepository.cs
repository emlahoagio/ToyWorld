using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class JoinedContestRepository : RepositoryBase<JoinedToContest>, IJoinedContestRepository
    {
        public JoinedContestRepository(DataContext context) : base(context)
        {
        }

        public void BandSubscribers(JoinedToContest join)
        {
            join.IsBan = true;
            Update(join);
        }

        public async Task Delete(int contest_id, bool trackChanges)
        {
            var joins = await FindByCondition(x => x.ContestId == contest_id, trackChanges).ToListAsync();

            foreach(var join in joins)
            {
                Delete(join);
            }
        }

        public async Task<List<AccountInList>> GetListSubscribers(int contest_id, bool trackChanges)
        {
            var accounts = await FindByCondition(x => x.ContestId == contest_id && x.IsBan == false && x.Account.Role != 1, trackChanges)
                .Include(x => x.Account)
                .OrderBy(x => x.Account.Name)
                .ToListAsync();

            var account_list = accounts.Select(x => new AccountInList
            {
                Avatar = x.Account.Avatar,
                Id = x.Account.Id,
                Name = x.Account.Name,
                Phone = x.Account.Phone,
                Status = x.Account.Status.ToString()
            }).ToList();

            return account_list;
        }

        public Task<JoinedToContest> GetSubsCriberToDelete(int contest_id, int account_id, bool trackChanges)
        {
            var result = FindByCondition(x => x.AccountId == account_id && x.ContestId == contest_id, trackChanges).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> IsBan(int contest_id, int account_id, bool trackChanges)
        {
            var joincontest = await FindByCondition(x => x.ContestId == contest_id && x.AccountId == account_id && x.IsBan == true, trackChanges)
                .FirstOrDefaultAsync();

            return joincontest != null;
        }

        public async Task<bool> IsJoinedToContest(int contest_id, int account_id, bool trackChanges)
        {
            var joinedContest = await FindByCondition(x => x.ContestId == contest_id && x.AccountId == account_id, trackChanges)
                .FirstOrDefaultAsync();

            return joinedContest == null ? false : true;
        }
    }
}
