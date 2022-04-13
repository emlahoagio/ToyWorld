using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IImageRepository
    {
        Task<List<RewardReturn>> GetImageForRewards(List<RewardReturn> rewards_post_no_image, bool trackChanges);
        Task<Image> GetImageById(int image_id, bool trackChanges);
        void Create(Image image);
        Task Delete(int image_id, bool trackChanges);
        void Delete(Image image);
        Task<Pagination<PostOfContestInList>> GetImageForPostOfContest(Pagination<PostOfContestInList> posts_no_rate_no_image, bool trackChanges);
        Task<List<ImageReturn>> GetImageForPrize(int prize_id, bool trackChanges);
        Task<Pagination<PrizeOfContest>> GetImageForPrizeList(Pagination<PrizeOfContest> pagignationPrize_no_image, bool trackChanges);
        Task<PrizeReturn> GetImageForPrize(PrizeReturn prize_no_image, bool trackChanges);
        Task<List<string>> GetImageOfTrading(int tradingPostId, bool trackChanges);
        Task<List<ImageReturn>> GetImageByPostId(int post_id, bool trackChanges);
        Task<List<TopSubmission>> GetImageForPostOfContest(List<TopSubmission> post, bool trackChanges);
        Task DeleteByPostOfContestId(int id, bool trackChanges);
        Task<List<ImageReturn>> GetImageByTradingPostId(int trading_id, bool trackChanges);
        Task<List<ImageReturn>> GetImageByBillId(int bill_id, bool trackChanges);
    }
}
