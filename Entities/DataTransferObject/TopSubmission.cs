using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class TopSubmission
    {
        public int Id  { get; set; }
        public string Content { get; set; }
        public List<ImageReturn> Images { get; set; }
        public double AverageStar { get; set; }
        public string OwnerName { get; set; }
    }
}
