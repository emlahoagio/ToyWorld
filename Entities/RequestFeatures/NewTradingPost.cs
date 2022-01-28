using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class NewTradingPost
    {
        public string Title { get; set; }
        public string ToyName { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string Exchange { get; set; }
        public decimal? Value { get; set; }
        public string Phone { get; set; }
        public List<string> ImagesLink { get; set; }
    }
}
