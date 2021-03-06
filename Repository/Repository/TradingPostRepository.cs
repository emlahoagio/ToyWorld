using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class TradingPostRepository : RepositoryBase<TradingPost>, ITradingPostRepository
    {
        public TradingPostRepository(DataContext context) : base(context)
        {
        }

        public void CreateTradingPost(NewTradingPostParameters tradingPost, int group_id, int account_id, int toy_id, int brand_id, int type_id, DateTime createTime)
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
                BrandId = brand_id,
                TypeId = type_id,
                GroupId = group_id,
                Images = tradingPost.ImagesLink.Select(x => new Image { Url = x }).ToList(),
                PostDate = createTime
            };

            Create(newTradingPost);
        }

        public void DisableOrEnable(TradingPost tradingPost, int disable_or_enable)
        {
            if (disable_or_enable == 0)
                tradingPost.IsDeleted = true;
            else tradingPost.IsDeleted = false;

            Update(tradingPost);
        }

        public void ExchangedTradingPost(TradingPost tradingPost)
        {
            tradingPost.IsExchanged = true;
            Update(tradingPost);
        }

        public async Task<Pagination<TradingPostInList>> GetTradingPostInGroupMember(int group_id, PagingParameters paging, bool trackChanges, int account_id)
        {
            var tradingPosts = await FindByCondition(x => x.IsDeleted == false && x.GroupId == group_id, trackChanges)
                .OrderByDescending(x => x.PostDate)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Toy)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                //.Include(x => x.ReactTradingPosts)
                .Include(x => x.Account)
                .ToListAsync();

            var count = await FindByCondition(x => x.IsDeleted == false && x.GroupId == group_id, trackChanges).CountAsync();

            var result = new Pagination<TradingPostInList>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = tradingPosts.Select(x => new TradingPostInList
                {
                    Address = x.Address,
                    Brand = x.Brand == null ? "Unknow" : x.Brand.Name,
                    Exchange = x.Trading,
                    Id = x.Id,
                    //NoOfReact = x.ReactTradingPosts.Count,
                    OwnerId = x.AccountId,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerName = x.Account.Name,
                    PostDate = x.PostDate,
                    ToyName = x.ToyName,
                    Type = x.Type == null ? "Unknow" : x.Type.Name,
                    //IsLikedPost = x.ReactTradingPosts.Where(y => y.AccountId == account_id).Count() == 0 ? false : true,
                    Value = x.Value,
                    Content = x.Content,
                    Title = x.Title,
                    Phone = x.Phone,
                    Status = x.Status,
                })
            };

            return result;
        }

        public async Task<TradingPost> GetTradingPostById(int tradingpost_id, bool trackChanges)
        {
            var tradingPost = await FindByCondition(x => x.Id == tradingpost_id, trackChanges).FirstOrDefaultAsync();

            if (tradingPost == null) return null;

            return tradingPost;
        }

        public async Task<UpdateTradingPost> GetUpdateDetail(int tradingpost_id, bool trackChanges)
        {
            var tradingPost = await FindByCondition(x => x.Id == tradingpost_id, trackChanges).FirstOrDefaultAsync();

            var result = new UpdateTradingPost
            {
                Address = tradingPost.Address,
                Content = tradingPost.Content,
                Exchange = tradingPost.Trading,
                Phone = tradingPost.Phone,
                Title = tradingPost.Title,
                ToyName = tradingPost.ToyName,
                Value = tradingPost.Value
            };

            return result;
        }

        public void UpdateTradingPost(UpdateTradingPostParameters update_infor, TradingPost tradingPost)
        {
            tradingPost.Title = update_infor.Title;
            tradingPost.ToyName = update_infor.ToyName;
            tradingPost.Content = update_infor.Content;
            tradingPost.Address = update_infor.Address;
            tradingPost.Trading = update_infor.Exchange;
            tradingPost.Value = update_infor.Value;
            tradingPost.Phone = update_infor.Phone;
            Update(tradingPost);
        }

        public async Task UpdateTradingStatus(int trading_post_id, int trading_status, bool trackChanges)
        {
            var trading_post = await FindByCondition(x => x.Id == trading_post_id, trackChanges).FirstOrDefaultAsync();

            trading_post.Status = trading_status;
            Update(trading_post);
        }

        public async Task<TradingPostDetail> GetDetail(int trading_post_id, int current_account_id, bool trackChanges)
        {
            var trading_post = await FindByCondition(x => x.Id == trading_post_id && x.IsDeleted == false, trackChanges)
                .Include(x => x.Brand)
                .Include(x => x.Account)
                .Include(x => x.ReactTradingPosts)
                .Include(x => x.Type)
                .FirstOrDefaultAsync();

            if (trading_post == null) return null;

            var result = new TradingPostDetail
            {
                Id = trading_post.Id,
                Address = trading_post.Address,
                BrandName = trading_post.Brand == null ? "Unkow brand" : trading_post.Brand.Name,
                Value = trading_post.Value,
                OwnerName = trading_post.Account.Name,
                OwnerId = trading_post.AccountId,
                Content = trading_post.Content,
                GroupId = trading_post.GroupId,
                IsReact = trading_post.ReactTradingPosts.Where(y => y.AccountId == current_account_id).Count() == 0 ? false : true,
                NumOfReact = trading_post.ReactTradingPosts.Count(),
                OwnerAvatar = trading_post.Account.Avatar,
                Phone = trading_post.Phone,
                PostDate = trading_post.PostDate,
                Status = trading_post.Status,
                Title = trading_post.Title,
                ToyId = trading_post.ToyId,
                ToyName = trading_post.ToyName,
                Trading = trading_post.Trading,
                TypeName = trading_post.Type == null ? "Unkow type" : trading_post.Type.Name
            };

            return result;
        }

        public async Task<int> GetOwnerById(int trading_post_id)
        {
            var result = await FindByCondition(x => x.Id == trading_post_id, false).FirstOrDefaultAsync();

            return result.AccountId.Value;
        }

        public async Task<Pagination<TradingManaged>> GetTradingPostForManager(int status, PagingParameters paging, bool trackChanges, int account_id)
        {
            var tradingPosts = new List<TradingPost>();
            int count = 0;
            if (status == 0)
            {
                tradingPosts = await FindAll(trackChanges)
                    .OrderByDescending(x => x.PostDate)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .Include(x => x.Brand)
                    .Include(x => x.Type)
                    .Include(x => x.Account)
                    .ToListAsync();
                count = FindAll(trackChanges).Count();
            }
            else if (status == 1)
            {
                tradingPosts = await FindByCondition(x => x.IsDeleted == true, trackChanges)
                    .OrderByDescending(x => x.PostDate)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .Include(x => x.Brand)
                    .Include(x => x.Type)
                    .Include(x => x.Account)
                    .ToListAsync();
                count = FindByCondition(x => x.IsDeleted == true, trackChanges).Count();
            }
            else
            {
                tradingPosts = await FindByCondition(x => x.IsDeleted == false, trackChanges)
                    .OrderByDescending(x => x.PostDate)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .Include(x => x.Brand)
                    .Include(x => x.Type)
                    .Include(x => x.Account)
                    .ToListAsync();
                count = FindByCondition(x => x.IsDeleted == false, trackChanges).Count();
            }

            var result = new Pagination<TradingManaged>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = tradingPosts.Select(x => new TradingManaged
                {
                    Address = x.Address,
                    Brand = x.Brand == null ? "Unknow" : x.Brand.Name,
                    Exchange = x.Trading,
                    Id = x.Id,
                    NoOfReact = x.ReactTradingPosts.Count,
                    OwnerId = x.AccountId,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerName = x.Account.Name,
                    PostDate = x.PostDate,
                    ToyName = x.ToyName,
                    Type = x.Type == null ? "Unknow" : x.Type.Name,
                    IsLikedPost = x.ReactTradingPosts.Where(y => y.AccountId == account_id).Count() == 0 ? false : true,
                    Value = x.Value,
                    Content = x.Content,
                    Title = x.Title,
                    Phone = x.Phone,
                    Status = x.Status,
                    IsDisabled = x.IsDeleted
                })
            };

            return result;
        }

        public async Task<int> GetNumOfReact(int trading_post_id, bool trackChanges)
        {
            var trading = await FindByCondition(x => x.Id == trading_post_id, trackChanges)
                .Include(x => x.ReactTradingPosts)
                .FirstOrDefaultAsync();

            return trading.ReactTradingPosts.Count();
        }

        public async Task<Pagination<TradingPostInList>> GetTradingByGroup(int account_id, List<int> groups, PagingParameters paging, bool trackChanges)
        {
            var tradings = await FindByCondition(x => x.PostDate >= DateTime.UtcNow.AddMonths(-1) && groups.Contains(x.GroupId) && x.IsDeleted == false, trackChanges)
                .OrderByDescending(x => x.PostDate)
                .Skip((paging.PageNumber-1)*paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .Include(x => x.Account)
                .Include(x => x.ReactTradingPosts)
                .ToListAsync();

            return new Pagination<TradingPostInList>
            {
                Count = await FindByCondition(x => x.PostDate >= DateTime.UtcNow.AddMonths(-1) && groups.Contains(x.GroupId) && x.IsDeleted == false, trackChanges).CountAsync(),
                Data = tradings.Select(x => new TradingPostInList
                {
                    Address = x.Address,
                    Brand = x.Brand.Name,
                    Content = x.Content,
                    Exchange = x.Trading,
                    Id = x.Id,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerId = x.AccountId,
                    OwnerName = x.Account.Name,
                    IsLikedPost = x.ReactTradingPosts.Where(x => x.AccountId == account_id).Count() != 0,
                    NoOfReact = x.ReactTradingPosts.Count(),
                    Phone = x.Phone,
                    PostDate = x.PostDate,
                    Status = x.Status,
                    Title = x.Title,
                    ToyName = x.ToyName,
                    Type = x.Type.Name,
                    Value = x.Value
                }),
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
        }

        public async Task<int> GetIdOfCreatedTrading(DateTime createTime, bool trackChanges)
        {
            var id = await FindByCondition(x => x.PostDate == createTime, trackChanges).Select(x => x.Id).FirstOrDefaultAsync();

            return id;
        }

        public async Task<DataForMess> GetDataForTradingMess(int tradingpostId)
        {
            var tradingPost = await FindByCondition(x => x.Id == tradingpostId, false)
                .FirstOrDefaultAsync();
            if (tradingPost == null)
            {
                return null;
            }
            DataForMess result = new DataForMess
            {
                Title = tradingPost.Title,
                ToyName = tradingPost.ToyName
            };

            return result;
        }

        public async Task<Pagination<TradingPostInList>> GetTradingPostOfAccount(int current_account_id, PagingParameters paging, bool trackChanges, int account_id)
        {
            var tradingPosts = await FindByCondition(x => x.IsDeleted == false && x.AccountId == account_id, trackChanges)
                .OrderByDescending(x => x.PostDate)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                //.Include(x => x.ReactTradingPosts)
                .Include(x => x.Account)
                .ToListAsync();

            var count = await FindByCondition(x => x.IsDeleted == false && x.AccountId == account_id, trackChanges).CountAsync();

            var result = new Pagination<TradingPostInList>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = tradingPosts.Select(x => new TradingPostInList
                {
                    Address = x.Address,
                    Brand = x.Brand == null ? "Unknow" : x.Brand.Name,
                    Exchange = x.Trading,
                    Id = x.Id,
                    //NoOfReact = x.ReactTradingPosts.Count,
                    OwnerId = x.AccountId,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerName = x.Account.Name,
                    PostDate = x.PostDate,
                    ToyName = x.ToyName,
                    Type = x.Type == null ? "Unknow" : x.Type.Name,
                    //IsLikedPost = x.ReactTradingPosts.Where(y => y.AccountId == account_id).Count() == 0 ? false : true,
                    Value = x.Value,
                    Content = x.Content,
                    Title = x.Title,
                    Phone = x.Phone,
                    Status = x.Status,
                })
            };

            return result;
        }

        public async Task<List<TradingForSearch>> GetAllTrading(bool trackChanges)
        {
            var tradings = await FindByCondition(x => x.IsDeleted == false, trackChanges)
                .OrderByDescending(x => x.Id)
                .Take(200)
                .Select(x => new TradingForSearch
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title
                })
                .ToListAsync();

            return tradings;
        }
    }
}
