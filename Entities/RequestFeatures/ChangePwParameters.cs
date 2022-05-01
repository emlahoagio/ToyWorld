using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class ChangePwParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string old_password { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string new_password { get; set; }
    }
}
