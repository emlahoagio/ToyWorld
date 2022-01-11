using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
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
