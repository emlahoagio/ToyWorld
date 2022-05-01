using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewGroupParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
