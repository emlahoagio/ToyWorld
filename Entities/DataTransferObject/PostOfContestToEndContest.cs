using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class PostOfContestToEndContest
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public double SumOfStart { get; set; }
    }
}
