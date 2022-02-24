using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class ProposalCreateContest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int? MinRegistration { get; set; }
        public int? MaxRegistration { get; set; }
        public int? Duration { get; set; }
        public string BrandName { get; set; }
        public string TypeName { get; set; }
    }
}
