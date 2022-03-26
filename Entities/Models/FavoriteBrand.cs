using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class FavoriteBrand
    {
        public int AccountId { get; set; }
        public int BrandId { get; set; }

        public virtual Account Account { get; set; }
        public virtual Brand Brand { get; set; }
    }
}
