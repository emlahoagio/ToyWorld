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
    }
}
