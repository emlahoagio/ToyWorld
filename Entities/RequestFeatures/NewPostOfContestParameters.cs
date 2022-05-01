using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewPostOfContestParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Content { get; set; }
        public List<string> ImagesUrl { get; set; }
    }
}
