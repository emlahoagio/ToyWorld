using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewAccountParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Name { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
