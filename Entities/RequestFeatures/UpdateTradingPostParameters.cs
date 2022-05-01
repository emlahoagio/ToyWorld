using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class UpdateTradingPostParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Title { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string ToyName { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Content { get; set; }
        [Required]
        public string Address { get; set; }
        public string Exchange { get; set; }
        public decimal Value { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
