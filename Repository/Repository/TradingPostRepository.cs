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
    public class TradingPostRepository : RepositoryBase<TradingPost>, ITradingPostRepository
    {
        public TradingPostRepository(DataContext context) : base(context)
        {
        }

        public void CreateTradingPost(NewTradingPost tradingPost, int group_id, int account_id, int toy_id)
        {
            var newTradingPost = new TradingPost
            {
                AccountId = account_id,
                Address = tradingPost.Address,
                Content = tradingPost.Content,
                Phone = tradingPost.Phone,
                Title = tradingPost.Title,
                ToyName = tradingPost.ToyName,
                Trading = tradingPost.Exchange,
                Value = tradingPost.Value,
                IsDeleted = false,
                IsExchanged = false,
                ToyId = toy_id,
                Images = tradingPost.ImagesLink.Select(x => new Image { Url = x }).ToList()
            };

            Create(newTradingPost);
        }

        public async Task<Pagination<TradingPostInList>> GetList(PagingParameters paging, bool trackChanges)
        {
            var tradingPosts = await FindByCondition(x => x.IsExchanged == false && x.IsDeleted == false, trackChanges)
                .Include(x => x.Toy).ThenInclude(x => x.Brand)
                .Include(x => x.Toy).ThenInclude(x => x.Type)
                .Include(x => x.ReactTradingPosts)
                .Include(x => x.Account)
                .Include(x => x.Comments)
                .OrderBy(x => x.PostDate)
                .ToListAsync();

            var count = tradingPosts.Count;

            var pagingList = tradingPosts.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            var result = new Pagination<TradingPostInList>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = tradingPosts.Select(x => new TradingPostInList
                {
                    Address = x.Address,
                    Brand = x.Toy.Brand.Name,
                    Exchange = x.Trading,
                    Id = x.Id,
                    Images = x.Images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList(),
                    NoOfComment = x.Comments.Count,
                    NoOfReact = x.ReactTradingPosts.Count,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerName = x.Account.Name,
                    PostDate = x.PostDate,
                    ToyName = x.ToyName,
                    Type = x.Toy.Type.Name
                })
            };

            return result;
        }
    }
}
