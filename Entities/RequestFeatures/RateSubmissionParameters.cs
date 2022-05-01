using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class RateSubmissionParameters
    {
        [Required]
        public double NumOfStar { get; set; }
        public string Note { get; set; }
    }
}
