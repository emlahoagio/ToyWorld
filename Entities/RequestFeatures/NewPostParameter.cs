using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewPostParameter
    {
        public string Content { get; set; }
        [Required]
        public int GroupId { get; set; }
        public List<string> ImagesLink { get; set; }
    }
}
