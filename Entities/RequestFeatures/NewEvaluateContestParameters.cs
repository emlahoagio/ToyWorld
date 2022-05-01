using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewEvaluateContestParameters
    {
        [Required]
        [MaxLength(1)]
        public int NumOfStar { get; set; }
        public string Comment { get; set; }
    }
}
