using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class AccountSystemParameters
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string Password { get; set; }
    }
}
