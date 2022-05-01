using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class CreateProposalModel
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Reason { get; set; }
        [Required]
        public int GroupId { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Rule { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Slogan { get; set; }
    }
}
