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

        public async Task<Pagination<PostOfContestInList>> GetPostOfContest(int contest_id, PagingParameters paging, bool trackChanges)
        {
            var posts = await FindByCondition(x => x.ContestId == contest_id && x.Status == 1, trackChanges)
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
                OwnerName = x.Account.Name,
                OwnerId = x.AccountId
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
            var list_post = await FindByCondition(x => x.ContestId == contest_id && x.Status == 1, trackChanges)
                .ToListAsync();

            var result = list_post.Select(x => new PostOfContestToEndContest
            {
                AccountId = x.AccountId,
                Id = x.Id,
                SumOfStart = x.Rates.Select(x => x.NumOfStar).Sum()
            }).OrderByDescending(y => y.SumOfStart).ToList();

            return result;
        }

        public async Task<List<int>> GetIdOfPost(int contest_id, bool trackChanges)
        {
            var idList = await FindByCondition(X => X.ContestId == contest_id && X.Status == 1, trackChanges)
                .Select(x => x.Id)
                .ToListAsync();

            return idList;
        }

        public async Task<List<TopSubmission>> GetPostOfContestById(List<int> ids, bool trackchanges)
        {
            var result = new List<TopSubmission>();
            foreach(var id in ids)
            {
                var data = await FindByCondition(x => x.Id == id && x.Status == 1, trackchanges)
                    .Include(x => x.Account)
                    .Select(x => new TopSubmission
                    {
                        Id = x.Id,
                        Content = x.Content,
                        OwnerName = x.Account.Name
                    }).FirstOrDefaultAsync();
                result.Add(data);
            }
            return result;
        }

        public async Task<List<PostOfContest>> GetPostToDelete(int contest_id, int account_id, bool trackChanges)
        {
            var posts = await FindByCondition(x => x.ContestId == contest_id && x.AccountId == account_id, trackChanges)
                .ToListAsync();

            return posts;
        }

        public async Task<PostOfContest> GetPostOfContestById(int post_of_contest_id, bool trackchanges)
        {
            var result = await FindByCondition(x => x.Id == post_of_contest_id, trackchanges).FirstOrDefaultAsync();

            return result;
        }

        public async Task<bool> IsReachPostLimit(int accountId, int contest_id, bool trackChanges)
        {
            var numOfPost = await FindByCondition(x => x.AccountId == accountId && x.ContestId == contest_id, trackChanges).CountAsync();

            return numOfPost >= 3;
        }

        public async Task<Pagination<PostOfContestManaged>> GetPostByContestId(int contest_id, PagingParameters paging, bool trackChanges)
        {
            var post = await FindByCondition(x => x.ContestId == contest_id && x.Status == 0, trackChanges)
                .OrderByDescending(x => x.DateCreate)
                .Skip((paging.PageNumber-1)*paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Account)
                .ToListAsync();

            var result = new Pagination<PostOfContestManaged>
            {
                Count = await FindByCondition(x => x.ContestId == contest_id, trackChanges).CountAsync(),
                Data = post.Select(x => new PostOfContestManaged
                {
                    Content = x.Content,
                    ContestId = x.ContestId,
                    DateCreate = x.DateCreate,
                    Id = x.Id,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerId = x.AccountId,
                    OwnerName = x.Account.Name,
                    Status = x.Status
                }).ToList(),
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public async Task<PostOfContest> GetById(int post_of_contest_id, bool trackChanges)
        {
            var post = await FindByCondition(x => x.Id == post_of_contest_id, trackChanges).FirstOrDefaultAsync();

            return post;
        }

        public void Approve(PostOfContest post)
        {
            post.Status = 1;
            Update(post);
        }

        public void Deny(PostOfContest post)
        {
            post.Status = 2;
            Update(post);
        }
    }
}
