using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class ToyDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Description { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
        public string CoverImage { get; set; }
        public List<ImageReturn> ImagesOfToy { get; set; }
    }
}
