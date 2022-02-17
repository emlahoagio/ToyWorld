using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class NewPrizeParameters
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public List<String> Images { get; set; }
    }
}
