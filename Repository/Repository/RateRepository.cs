using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RateRepository : RepositoryBase<Rate>, IRateRepository
    {
        public RateRepository(DataContext context) : base(context)
        {
        }

        public async Task Delete(List<int> listPostId, bool trackChanges)
        {
            foreach(var postId in listPostId)
            {
                var rates = await FindByCondition(x => x.PostOfContestId == postId, trackChanges).ToListAsync();
                foreach (var rate in rates)
                    Delete(rate);
            }
        }

        public async Task DeleteRateOfPost(PostOfContest post, bool trackChanges)
        {
            var rates = await FindByCondition(x => x.PostOfContestId == post.Id, trackChanges).ToListAsync();

            if(rates.Count != 0)
            {
                foreach(var rate in rates)
                {
                    Delete(rate);
                }
            }
        }

        public async Task<List<int>> GetIdOfPostInTop10(List<int> submissionsId, List<int> idHasRewards, bool trackChanges)
        {
            var posts = new List<PostOnTop>();

            foreach(var id in submissionsId)
            {
                if (!idHasRewards.Contains(id))
                {
                    var sumOfStar = await FindByCondition(x => x.PostOfContestId == id, trackChanges)
                        .Select(x => x.NumOfStar).SumAsync();
                    posts.Add(new PostOnTop { Id = id, SumOfStar = sumOfStar });
                }
            }

            posts.OrderByDescending(x => x.SumOfStar);

            return posts.Select(x => x.Id).Take(10).ToList();
        }

        public async Task<List<int>> GetIdOfPostInTop3(List<int> submissionsId, bool trackChanges)
        {
            double top1 = 0, top2 = 0, top3 = 0;
            int idTop1 = 0, idTop2 = 0, idTop3 = 0;
            var posts = new List<PostOnTop>();
            //get data from db
            foreach (var id in submissionsId)
            {
                var sumOfStar = await FindByCondition(x => x.PostOfContestId == id, trackChanges)
                    .Select(x => x.NumOfStar).SumAsync();
                posts.Add(new PostOnTop { Id = id, SumOfStar = sumOfStar });
            }
            //find top 1
            foreach(var post in posts)
            {
                if(post.SumOfStar > top1)
                {
                    top1 = post.SumOfStar;
                    idTop1 = post.Id;
                }
            }
            //find top 2
            foreach (var post in posts)
            {
                if (post.SumOfStar > top2 && post.SumOfStar < top1)
                {
                    top2 = post.SumOfStar;
                    idTop2 = post.Id;
                }
            }
            //find top 3
            foreach (var post in posts)
            {
                if (post.SumOfStar > top3 && post.SumOfStar < top2)
                {
                    top3 = post.SumOfStar;
                    idTop3 = post.Id;
                }
            }
            return new List<int> { idTop1, idTop2, idTop3 };
        }

        public async Task<Pagination<PostOfContestInList>> GetRateForPostOfContest(Pagination<PostOfContestInList> post_no_rate, int account_id, bool trackChanges)
        {
            var data = new List<PostOfContestInList>();

            foreach(var post in post_no_rate.Data)
            {
                var rates = await FindByCondition(x => x.PostOfContestId == post.Id, trackChanges)
                    .Include(x => x.Account)
                    .ToListAsync();

                if (rates.Count != 0)
                {
                    post.Rates = rates.Select(x => new RateReturn
                    {
                        Id = x.Id,
                        Note = x.Note,
                        NumOfStar = x.NumOfStar,
                        OwnerAvatar = x.Account.Avatar,
                        OwnerId = x.AccountId.Value,
                        OwnerName = x.Account.Name
                    }).ToList();
                    post.AverageStar = rates.Select(x => x.NumOfStar).Count();
                    post.IsRated = rates.Where(x => x.AccountId.Value == account_id).Count() != 0;
                }else
                {
                    post.Rates = new List<RateReturn>();
                    post.AverageStar = 0;
                    post.IsRated = false;
                }
                data.Add(post);
            }

            post_no_rate.Data = data;
            return post_no_rate;
        }

        public async Task<List<TopSubmission>> GetRateForPostOfContest(List<TopSubmission> posts, bool trackChanges)
        {
            var result = new List<TopSubmission>();
            foreach(var post in posts)
            {
                var rates = await FindByCondition(x => x.PostOfContestId == post.Id, trackChanges)
                    .Select(x => x.NumOfStar).ToListAsync();
                if (rates.Count != 0)
                {
                    post.AverageStar = rates.Average();
                    post.SumOfStar = rates.Sum();
                }
                result.Add(post);
            }

            var orderResult = result.OrderByDescending(x => x.SumOfStar).ToList();

            return orderResult;
        }

        public async Task<bool> IsRated(int post_id, int account_id, bool trackChanges)
        {
            var rate = await FindByCondition(x => x.AccountId == account_id && x.PostOfContestId == post_id, trackChanges).FirstOrDefaultAsync();

            return rate == null ? false : true;
        }
    }
}
