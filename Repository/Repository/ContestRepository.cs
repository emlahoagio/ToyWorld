using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ContestRepository : RepositoryBase<Contest>, IContestRepository
    {
        public ContestRepository(DataContext context) : base(context)
        {

        }

        public async Task EndContest(int contestId, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contestId, trackChanges).FirstOrDefaultAsync();

            contest.EndDate = DateTime.Now;
            contest.Status = 4;

            Update(contest);
        }

        public async Task<ContestDetail> GetContestDetailInformation(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .Include(x => x.Evaluates).ThenInclude(y => y.Account)
                .FirstOrDefaultAsync();

            if (contest == null) return null;

            var result = new ContestDetail
            {
                BrandName = contest.Brand == null ? "Unknown" : contest.Brand.Name,
                CoverImage = contest.CoverImage,
                Description = contest.Description,
                EndDate = contest.EndDate,
                EndRegistration = contest.EndRegistration,
                Evaluates = contest.Evaluates.Select(x => new EvaluateInContestDetail
                {
                    AccountId = x.AccountId,
                    Comment = x.Comment,
                    NoOfStart = x.NoOfStart,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerName = x.Account.Name
                }).ToList(),
                Id = contest.Id,
                MaxRegistration = contest.MaxRegistration,
                MinRegistration = contest.MinRegistration,
                Slogan = contest.Slogan,
                StartDate = contest.StartDate,
                StartRegistration = contest.StartRegistration,
                Title = contest.Title,
                TypeName = contest.Type == null ? "Unknown" : contest.Type.Name,
                Status = contest.Status.Value,
                Rule = contest.Rule
            };

            return result;
        }

        public async Task<Pagination<ContestInGroup>> GetContestInGroup(int group_id, int account_id, bool trackChanges, PagingParameters paging)
        {
            var contest_in_group = await FindByCondition(x => x.GroupId == group_id, trackChanges)
                .ToListAsync();

            if (contest_in_group == null) return null;

            var count = contest_in_group.Count();

            var paging_contest = contest_in_group
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            var contestInGroup = paging_contest.Select(x => new ContestInGroup
            {
                CoverImage = x.CoverImage,
                Description = x.Description,
                EndDate = x.EndDate,
                EndRegistration = x.EndRegistration,
                Id = x.Id,
                Slogan = x.Slogan,
                MaxRegistration = x.MaxRegistration,
                MinRegistration = x.MinRegistration,
                StartDate = x.StartDate,
                StartRegistration = x.StartRegistration,
                Title = x.Title,
                Status = x.Status.Value
            }).ToList();

            var result = new Pagination<ContestInGroup>
            {
                Count = count,
                Data = contestInGroup,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public async Task<Contest> GetCreatedContest(int group_id, string title, DateTime? startRegistration, bool trackChanges)
        {
            var result = await FindByCondition(x => x.GroupId == group_id
                && x.Title == title
                && x.StartRegistration == startRegistration, trackChanges).FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<HighlightContest>> GetHightlightContest(bool trackChanges)
        {
            var listContest = await FindByCondition(c => c.Status == 1 || c.Status == 3, trackChanges)
                .OrderBy(x => x.EndRegistration)
                .ToListAsync();

            if (listContest == null)
                return null;

            var result = new List<HighlightContest>();

            foreach (Contest contest in listContest)
            {
                result.Add(new HighlightContest
                {
                    CoverImage = contest.CoverImage,
                    Id = contest.Id,
                    Slogan = contest.Slogan,
                    Title = contest.Title,
                });
            }

            return result;
        }

        public async Task StartRegistration(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            contest.Status = 1;

            Update(contest);
        }

        public async Task EndRegistration(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            contest.Status = 2;

            Update(contest);
        }

        public async Task StartContest(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            contest.Status = 3;

            Update(contest);
        }

        public async Task<bool> IsStartRegis(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            return contest.Status == 1;
        }

        public async Task<bool> IsOpenContest(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            return contest.Status != 0 && contest.Status != 4;
        }

        public async Task<Contest> GetEvaluateContest(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges)
                .Include(x => x.AccountJoined)
                .FirstOrDefaultAsync();

            return contest;
        }

        public async Task<Pagination<ContestInGroup>> GetContestByGroups(int account_id, List<int> groups, PagingParameters paging, bool trackChanges)
        {
            var contests = await FindByCondition(x => x.StartRegistration >= DateTime.UtcNow.AddMonths(-6) && groups.Contains(x.GroupId.Value), trackChanges)
                .OrderByDescending(x => x.Id)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .ToListAsync();

            return new Pagination<ContestInGroup>
            {
                Count = await FindByCondition(x => x.StartRegistration >= DateTime.UtcNow.AddMonths(-6) && groups.Contains(x.GroupId.Value), trackChanges).CountAsync(),
                Data = contests.Select(x => new ContestInGroup
                {
                    CoverImage = x.CoverImage,
                    Description = x.Description,
                    EndDate = x.EndDate,
                    EndRegistration = x.EndRegistration,
                    Id = x.Id,
                    Slogan = x.Slogan,
                    MaxRegistration = x.MaxRegistration,
                    MinRegistration = x.MinRegistration,
                    StartDate = x.StartDate,
                    StartRegistration = x.StartRegistration,
                    Title = x.Title,
                    Status = x.Status.Value
                }).ToList(),
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber
            };
        }

        public async Task<Pagination<ContestInGroup>> GetContestByStatus(int status, PagingParameters paging, bool trackChanges)
        {
            var contests = new List<Contest>();
            int count = 0;

            if (status == 0)
            {
                contests = await FindAll(trackChanges)
                    .OrderByDescending(x => x.Id)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();
                count = await FindAll(trackChanges).CountAsync();
            }
            else if (status == 1)
            {
                contests = await FindByCondition(x => x.Status == 4, trackChanges)
                    .OrderByDescending(x => x.Id)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();
                count = await FindByCondition(x => x.Status == 4, trackChanges).CountAsync();
            }
            else
            {
                contests = await FindByCondition(x => x.Status != 4 && x.Status != 0, trackChanges)
                    .OrderByDescending(x => x.Id)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();
                count = await FindByCondition(x => x.Status != 4 && x.Status != 0, trackChanges).CountAsync();
            }

            var result_data = contests.Select(x => new ContestInGroup
            {
                CoverImage = x.CoverImage,
                Description = x.Description,
                EndDate = x.EndDate,
                EndRegistration = x.EndRegistration,
                Id = x.Id,
                MaxRegistration = x.MaxRegistration,
                MinRegistration = x.MinRegistration,
                Slogan = x.Slogan,
                StartDate = x.StartDate,
                StartRegistration = x.StartRegistration,
                Title = x.Title,
                Status = x.Status.Value
            }).ToList();

            var result = new Pagination<ContestInGroup>
            {
                Count = count,
                Data = result_data,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public async Task Delete(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();
            Delete(contest);
        }

        public async Task<Pagination<ContestInGroup>> GetContestHighlightMb(int account_id, bool trackChanges, PagingParameters paging)
        {
            var contest_in_group = await FindByCondition(c => c.Status == 1 || c.Status == 3, trackChanges)
                .ToListAsync();

            if (contest_in_group == null) return null;

            var count = contest_in_group.Count();

            var paging_contest = contest_in_group
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            var contestInGroup = paging_contest.Select(x => new ContestInGroup
            {
                CoverImage = x.CoverImage,
                Description = x.Description,
                EndDate = x.EndDate,
                EndRegistration = x.EndRegistration,
                Id = x.Id,
                Slogan = x.Slogan,
                MaxRegistration = x.MaxRegistration,
                MinRegistration = x.MinRegistration,
                StartDate = x.StartDate,
                StartRegistration = x.StartRegistration,
                Title = x.Title,
                Status = x.Status.Value
            }).ToList();

            var result = new Pagination<ContestInGroup>
            {
                Count = count,
                Data = contestInGroup,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }
    }
}
