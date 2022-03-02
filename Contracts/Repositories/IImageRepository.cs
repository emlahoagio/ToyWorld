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
    }
}
