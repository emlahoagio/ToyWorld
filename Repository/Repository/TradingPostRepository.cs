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

        public void CreateTradingPost(NewTradingPostParameters tradingPost, int group_id, int account_id, int toy_id, int brand_id, int type_id)
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
                PostDate = DateTime.UtcNow
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
                .Include(x => x.Toy)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .Include(x => x.ReactTradingPosts)
                .Include(x => x.Account)
                .OrderByDescending(x => x.PostDate)
                .ToListAsync();

            var count = tradingPosts.Count;

            var pagingList = tradingPosts.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            var result = new Pagination<TradingPostInList>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = pagingList.Select(x => new TradingPostInList
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

        public async Task<Pagination<TradingManaged>> GetTradingPostForManager(int status, PagingParameters paging, bool trackChanges, int account_id)
        {
            var tradingPosts = new List<TradingPost>();
            int count = 0;
            if (status == 0)
            {
                tradingPosts = await FindAll(trackChanges)
                    .Include(x => x.Brand)
                    .Include(x => x.Type)
                    .Include(x => x.Account)
                    .OrderByDescending(x => x.PostDate)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();
                count = FindAll(trackChanges).Count();
            }
            else if (status == 1)
            {
                tradingPosts = await FindByCondition(x => x.IsDeleted == true, trackChanges)
                    .Include(x => x.Brand)
                    .Include(x => x.Type)
                    .Include(x => x.Account)
                    .OrderByDescending(x => x.PostDate)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
                    .ToListAsync();
                count = FindByCondition(x => x.IsDeleted == true, trackChanges).Count();
            }
            else
            {
                tradingPosts = await FindByCondition(x => x.IsDeleted == false, trackChanges)
                    .Include(x => x.Brand)
                    .Include(x => x.Type)
                    .Include(x => x.Account)
                    .OrderByDescending(x => x.PostDate)
                    .Skip((paging.PageNumber - 1) * paging.PageSize)
                    .Take(paging.PageSize)
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

        public async Task<Pagination<TradingPostInList>> GetTradingByBrandAndType(int account_id, List<Entities.Models.Type> types, List<Brand> brands, PagingParameters paging, bool trackChanges)
        {
            var tradings = await FindByCondition(x => x.PostDate >= DateTime.UtcNow.AddMonths(-2) && x.IsDeleted == false, trackChanges)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .Include(x => x.Account)
                .ToListAsync();

            //Account have no favorite
            if (types == null && brands == null)
            {
                return new Pagination<TradingPostInList>
                {
                    Count = tradings.Count,
                    Data = tradings.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize).Select(x => new TradingPostInList
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
                        Status = x.Status
                    }).ToList(),
                    PageSize = paging.PageSize,
                    PageNumber = paging.PageNumber
                };
            }

            var result = new List<TradingPost>();
            //select by brand
            if (brands != null)
            {
                foreach (var brand in brands)
                {
                    foreach (var trading in tradings)
                    {
                        if (trading.BrandId == brand.Id)
                        {
                            result.Add(trading);
                            tradings.Remove(trading);
                        }
                    }
                }
            }
            //select by type
            if (types != null)
            {
                foreach (var type in types)
                {
                    foreach (var trading in tradings)
                    {
                        if (trading.TypeId == type.Id)
                        {
                            result.Add(trading);
                            tradings.Remove(trading);
                        }
                    }
                }
            }
            var count = result.Count;
            //paging result
            var paging_result = result.Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize)
                .Select(x => new TradingPostInList
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
                    Status = x.Status
                }).ToList();
            return new Pagination<TradingPostInList>
            {
                Count = count,
                Data = paging_result,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };
        }
    }
}
