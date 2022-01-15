using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class ToyInList : Object
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
        public string CoverImage { get; set; }
    }
}
