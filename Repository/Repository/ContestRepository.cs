using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                .Include(x => x.Images)
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
                Images = contest.Images.Select(x => new ImageReturn
                {
                    Id = x.Id,
                    Url = x.Url
                }).ToList(),
                IsOnlineContest = contest.IsOnlineContest,
                MaxRegistration = contest.MaxRegistration,
                MinRegistration = contest.MinRegistration,
                RegisterCost = contest.RegisterCost,
                Slogan = contest.Slogan,
                StartDate = contest.StartDate,
                StartRegistration = contest.StartRegistration,
                Title = contest.Title,
                TypeName = contest.Type == null ? "Unknown" : contest.Type.Name,
                Venue = contest.Venue
            };

            return result;
        }

        public async Task<Pagination<ContestInGroup>> GetContestInGroup(int group_id, int account_id, bool trackChanges, PagingParameters paging)
        {
            var contest_in_group = await FindByCondition(x => x.GroupId == group_id && x.StartRegistration >= DateTime.Now.AddDays(-5), trackChanges)
                .Include(x => x.Images)
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
                ProposalId = x.ProposalId,
                StartDate = x.StartDate,
                StartRegistration = x.StartRegistration,
                Title = x.Title,
                Venue = x.Venue,
                Images = x.Images.Select(y => new ImageReturn
                {
                    Id = y.Id,
                    Url = y.Url
                }).ToList(),
                IsJoined = x.AccountJoined.Where(x => x.AccountId == account_id).Count() == 0 ? false : true
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

        public async Task<IEnumerable<HighlightContest>> getHightlightContest(bool trackChanges)
        {
            var listContest = await FindByCondition(c => c.CanAttempt == true && c.EndRegistration >= DateTime.Now, trackChanges)
                .OrderBy(x => x.EndRegistration)
                .Take(4)
                .ToListAsync();

            if(listContest == null)
                return null;

            var result = new List<HighlightContest>();

            foreach(Contest contest in listContest)
            {
                result.Add(new HighlightContest
                {
                    CoverImage = contest.CoverImage,
                    Id = contest.Id,
                    Slogan = contest.Slogan,
                    Title = contest.Title,
                    Venue = contest.Venue
                });
            }

            return result;
        }

        public async Task StartRegistration(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            contest.CanAttempt = true;
            contest.Status = 1;

            Update(contest);
        } 
        
        public async Task EndRegistration(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            contest.CanAttempt = false;
            contest.Status = 2;

            Update(contest);
        }
        
        public async Task StartContest(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            contest.Status = 3;

            Update(contest);
        }

        public async Task<bool> CanJoin(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            return contest.CanAttempt;
        }

        public async Task<bool> IsOpenContest(int contest_id, bool trackChanges)
        {
            var contest = await FindByCondition(x => x.Id == contest_id, trackChanges).FirstOrDefaultAsync();

            return contest.Status == 3 ? true : false;
        }
    }
}
