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
        Task<TradingPostDetail> GetImageForTradingDetail(TradingPostDetail trading_post_detail_no_image, bool trackChanges);
        void Create(Image image);
        Task Delete(int image_id, bool trackChanges);
        void Delete(Image image);
    }
}
