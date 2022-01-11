using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class HighlightContest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Venue { get; set; }
        public string Slogan { get; set; }
        public string CoverImage { get; set; }
    }
}
