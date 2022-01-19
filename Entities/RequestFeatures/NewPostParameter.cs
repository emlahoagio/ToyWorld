using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class NewPostParameter
    {
        public string Content { get; set; }
        public int AccountId { get; set; }
        public int GroupId { get; set; }
        public int ToyId { get; set; }
        public List<string> ImagesLink { get; set; }
    }
}
