using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewRateSellerParameters
    {
        [Required]
        public double NumOfStar { get; set; }
        public string Content { get; set; }
    }
}
