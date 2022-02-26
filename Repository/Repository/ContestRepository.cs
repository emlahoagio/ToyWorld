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

        public async Task<Pagination<ContestInGroup>> GetContestInGroup(int group_id, bool trackChanges, PagingParameters paging)
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
    }
}
