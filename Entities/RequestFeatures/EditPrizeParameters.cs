using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class EditPrizeParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        [MinLength(6)]
        public string Description { get; set; }
        [Required]
        public double? Value { get; set; }
    }
}
