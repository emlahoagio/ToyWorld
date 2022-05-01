using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class WishlistRemove
    {
        [Required]
        public int Id { get; set; }
    }
}
