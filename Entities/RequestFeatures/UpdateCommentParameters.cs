using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class UpdateCommentParameters
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Content { get; set; }
    }
}
