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
    public class PostOfContestRepository : RepositoryBase<PostOfContest>, IPostOfContestRepository
    {
        public PostOfContestRepository(DataContext context) : base(context)
        {
        }

        public async Task Delete(int contestId, bool trackChanges)
        {
            var posts = await FindByCondition(x => x.ContestId == contestId, trackChanges).ToListAsync();
            
            foreach(var post in posts)
            {
                Delete(post);
            }
        }

        public async Task<int> GetOwnerByPostOfContestId(int id)
        {
            int result = 0;
            var postOfContest = await FindByCondition(x => x.Id == id, false).FirstOrDefaultAsync();
            result = postOfContest.AccountId;
            return result;
        }

        public async Task<Pagination<PostOfContestInList>> GetPostOfContest(int contest_id, PagingParameters paging, int current_account_id, bool trackChanges)
        {
            var posts = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Include(x => x.Account)
                .OrderByDescending(x => x.DateCreate)
                .ToListAsync();

            var count = posts.Count;

            var paging_result = posts.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var post_in_list = paging_result.Select(x => new PostOfContestInList
            {
                Content = x.Content,
                Id = x.Id,
                OwnerAvatar = x.Account.Avatar,
                OwnerName = x.Account.Name
            }).ToList();

            var result = new Pagination<PostOfContestInList>
            {
                Count = count,
                Data = post_in_list,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public async Task<List<int>> GetPostOfContest(int contest_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.ContestId == contest_id, trackChanges).Select(x => x.Id).ToListAsync();

            return result;
        }

        public async Task<List<PostOfContestToEndContest>> GetPostOfContestForEndContest(int contest_id, bool trackChanges)
        {
            var list_post = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .ToListAsync();

            var result = list_post.Select(x => new PostOfContestToEndContest
            {
                AccountId = x.AccountId,
                Id = x.Id,
                SumOfStart = x.Rates.Select(x => x.NumOfStar).Sum()
            }).OrderByDescending(y => y.SumOfStart).ToList();

            return result;
        }
    }
}
