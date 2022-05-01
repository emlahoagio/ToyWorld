using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class AddWishlistParameters
    {
        [Required]
        public List<int> GroupIds { get; set; }
    }
}
