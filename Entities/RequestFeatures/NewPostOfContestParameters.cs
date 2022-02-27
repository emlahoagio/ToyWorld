using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.RequestFeatures
{
    public class NewPostOfContestParameters
    {
        public string Content { get; set; }
        public List<string> ImagesUrl { get; set; }
    }
}
