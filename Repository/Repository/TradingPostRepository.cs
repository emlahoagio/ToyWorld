using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
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
                Images = tradingPost.ImagesLink.Select(x => new Image { Url = x }).ToList()
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

        public async Task<Pagination<TradingPostInList>> GetTradingPostInGroup(int group_id, PagingParameters paging, bool trackChanges, int account_id)
        {
            var tradingPosts = await FindByCondition(x => x.IsExchanged == false && x.IsDeleted == false && x.GroupId == group_id, trackChanges)
                .Include(x => x.Toy)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .Include(x => x.ReactTradingPosts)
                .Include(x => x.Images)
                .Include(x => x.Account)
                .Include(x => x.Comments)
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
                    Images = x.Images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList(),
                    NoOfComment = x.Comments.Count,
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
                    Phone = x.Phone
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
                .Include(x => x.Comments).ThenInclude(x => x.ReactComments)
                .Include(x => x.Comments).ThenInclude(x => x.Account)
                .Include(x => x.Account)
                .Include(x => x.Images)
                .Include(x => x.ReactTradingPosts)
                .Include(x => x.Type)
                .FirstOrDefaultAsync();

            var result = new TradingPostDetail
            {
                Id = trading_post.Id,
                Address = trading_post.Address,
                BrandName = trading_post.Brand == null ? "Unkow brand" : trading_post.Brand.Name,
                Comment = trading_post.Comments.Select(y => new CommentReturn
                {
                    CommentDate = y.CommentDate,
                    Content = y.Content,
                    Id = y.Id,
                    NumOfReact = y.ReactComments.Count,
                    OwnerAvatar = y.Account.Avatar,
                    OwnerId = y.AccountId.Value,
                    OwnerName = y.Account.Name
                }).ToList(),
                Value = trading_post.Value,
                OwnerName = trading_post.Account.Name,
                OwnerId = trading_post.AccountId,
                Content = trading_post.Content,
                GroupId = trading_post.GroupId,
                Images = trading_post.Images.Select(y => new ImageReturn
                {
                    Id = y.Id,
                    Url = y.Url
                }).ToList(),
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
                .Include(x => x.ToyName)
                .FirstOrDefaultAsync();

            DataForMess result = new DataForMess
            {
                Title = tradingPost.Title,
                ToyName = tradingPost.ToyName
            };
            return result;
        }
    }
}
