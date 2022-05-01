using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewReward
    {
        [Required]
        public int PostOfContestId { get; set; }
        [Required]
        public int PrizeId { get; set; }
    }
}
