using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class NewProposalParameters
    {
        public string Title { get; set; }
        public int MinRegister { get; set; }
        public int MaxRegister { get; set; }
        public string Description { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
        public int TakePlace { get; set; }
        public List<string> ImagesUrl { get; set; }
    }
}
