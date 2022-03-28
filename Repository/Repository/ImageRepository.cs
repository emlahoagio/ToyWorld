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
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(DataContext context) : base(context)
        {
        }

        public async Task<Image> GetImageById(int image_id, bool trackChanges)
        {
            var image = await FindByCondition(x => x.Id == image_id, trackChanges).FirstOrDefaultAsync();

            return image;
        }

        public async Task<List<RewardReturn>> GetImageForRewards(List<RewardReturn> rewards_post_no_image, bool trackChanges)
        {
            var result = new List<RewardReturn>();
            
            foreach(var reward in rewards_post_no_image)
            {
                var images = await FindByCondition(x => x.PostOfContestId == reward.Post.Id, trackChanges)
                    .Select(x => new ImageReturn 
                    {
                        Id = x.Id,
                        Url = x.Url
                    }).ToListAsync();
                reward.Post.Images = images;
                result.Add(reward);
            }

            return result;
        }

        public async Task Delete(int image_id, bool trackChanges)
        {
            var image = await FindByCondition(x => x.Id == image_id, trackChanges).FirstOrDefaultAsync();

            Delete(image);
        }

        public async Task<TradingPostDetail> GetImageForTradingDetail(TradingPostDetail trading_post_detail_no_image, bool trackChanges)
        {
            trading_post_detail_no_image.Images = await FindByCondition(x => x.TradingPostId == trading_post_detail_no_image.Id, trackChanges)
                .Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToListAsync();

            return trading_post_detail_no_image;
        }

        public async Task<PostDetail> GetImageForPostDetail(PostDetail result_no_image, bool trackChanges)
        {
            var images = await FindByCondition(x => x.PostId == result_no_image.Id, trackChanges).ToListAsync();

            result_no_image.Images = images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList();

            return result_no_image;
        }

        public async Task<Pagination<PostInList>> GetImageForListPost(Pagination<PostInList> result_no_image, bool trackChanges)
        {
            var data = new List<PostInList>();

            foreach(var post in result_no_image.Data)
            {
                var images = await FindByCondition(x => x.PostId == post.Id, trackChanges).ToListAsync();

                if (images != null) post.Images = images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList();

                data.Add(post);
            }

            result_no_image.Data = data;
            return result_no_image;
        }

        public async Task<Pagination<TradingPostInList>> GetImageForListTradingPost(Pagination<TradingPostInList> result_no_image, bool trackChanges)
        {
            var data = new List<TradingPostInList>();

            foreach (var post in result_no_image.Data)
            {
                var images = await FindByCondition(x => x.TradingPostId == post.Id, trackChanges).ToListAsync();

                if (images != null) post.Images = images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList();

                data.Add(post);
            }

            result_no_image.Data = data;
            return result_no_image;
        }

        public async Task<Pagination<PostOfContestInList>> GetImageForPostOfContest(Pagination<PostOfContestInList> posts_no_rate_no_image, bool trackChanges)
        {
            var data = new List<PostOfContestInList>();

            foreach (var post in posts_no_rate_no_image.Data)
            {
                var images = await FindByCondition(x => x.PostOfContestId == post.Id, trackChanges).ToListAsync();

                if (images != null) post.Images = images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList();

                data.Add(post);
            }

            posts_no_rate_no_image.Data = data;
            return posts_no_rate_no_image;
        }

        public async Task<Pagination<WaitingPost>> GetImageForWaitingPostDetail(Pagination<WaitingPost> result_no_image, bool trackChanges)
        {
            var data = new List<WaitingPost>();

            foreach (var post in result_no_image.Data)
            {
                var images = await FindByCondition(x => x.PostId == post.Id, trackChanges).ToListAsync();

                if (images != null) post.Images = images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList();

                data.Add(post);
            }

            result_no_image.Data = data;
            return result_no_image;
        }

        public async Task<Pagination<PrizeOfContest>> GetImageForPrizeList(Pagination<PrizeOfContest> pagignationPrize_no_image, bool trackChanges)
        {
            var data = new List<PrizeOfContest>();

            foreach (var prize in pagignationPrize_no_image.Data)
            {
                var images = await FindByCondition(x => x.PrizeId == prize.Id, trackChanges).ToListAsync();

                if (images != null) prize.Images = images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList();

                data.Add(prize);
            }

            pagignationPrize_no_image.Data = data;
            return pagignationPrize_no_image;
        }

        public async Task<PrizeReturn> GetImageForPrize(PrizeReturn prize_no_image, bool trackChanges)
        {
            var images = await FindByCondition(x => x.PrizeId == prize_no_image.Id, trackChanges).ToListAsync();

            prize_no_image.Images = images.Select(x => new ImageReturn { Id = x.Id, Url = x.Url }).ToList();

            return prize_no_image;
        }
    }
}
