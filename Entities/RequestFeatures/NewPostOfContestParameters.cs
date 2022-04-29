using System.Collections.Generic;

namespace Entities.RequestFeatures
{
    public class NewPostOfContestParameters
    {
        public string Content { get; set; }
        public List<string> ImagesUrl { get; set; }
    }
}
