using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewFeedback
    {
        [Required]
        public string Content { get; set; }
    }
}
