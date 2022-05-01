using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewPrizeParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
