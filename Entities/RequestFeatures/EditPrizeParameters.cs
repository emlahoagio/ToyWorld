using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class EditPrizeParameters
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? Value { get; set; }
    }
}
