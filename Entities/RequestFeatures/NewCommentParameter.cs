using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewCommentParameter
    {
        [Required]
        public int PostId { get; set; }
        [Required]
        public string Content { get; set; }
    }
}
