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

        public async Task<Pagination<PostOfContestInList>> GetPostOfContest(int contest_id, PagingParameters paging, int current_account_id, bool trackChanges)
        {
            var posts = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .Include(x => x.Images)
                .Include(x => x.Account)
                .Include(x => x.Rates).ThenInclude(y => y.Account)
                .OrderByDescending(x => x.DateCreate)
                .ToListAsync();

            var count = posts.Count;

            var paging_result = posts.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize);

            var post_in_list = paging_result.Select(x => new PostOfContestInList
            {
                Content = x.Content,
                Id = x.Id,
                Images = x.Images.Select(y => new ImageReturn
                {
                    Id = y.Id,
                    Url = y.Url
                }).ToList(),
                OwnerAvatar = x.Account.Avatar,
                OwnerName = x.Account.Name,
                AverageStar = x.Rates.Select(x => x.NumOfStart).ToList().Average(),
                IsRated = x.Rates.Where(x => x.AccountId.Value == current_account_id).Count() != 0 ? true : false,
                Rates = x.Rates.Select(y => new RateReturn
                {
                    Id = y.Id,
                    Note = y.Note,
                    NumOfStar = y.NumOfStart,
                    OwnerAvatar = y.Account.Avatar,
                    OwnerName = y.Account.Name,
                    OwnerId = y.AccountId.Value
                }).ToList()
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

        public async Task<List<PostOfContestToEndContest>> GetPostOfContestForEndContest(int contest_id, bool trackChanges)
        {
            var list_post = await FindByCondition(x => x.ContestId == contest_id, trackChanges)
                .ToListAsync();

            var result = list_post.Select(x => new PostOfContestToEndContest
            {
                AccountId = x.AccountId,
                Id = x.Id,
                SumOfStart = x.Rates.Select(x => x.NumOfStart).Sum()
            }).OrderByDescending(y => y.SumOfStart).ToList();

            return result;
        }
    }
}
