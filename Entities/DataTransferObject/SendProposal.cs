using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class SendProposal
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? MinRegister { get; set; }
        public int? MaxRegister { get; set; }
        public string Location { get; set; }
        /// <summary>
        /// Unit: Days
        /// </summary>
        public int Duration { get; set; }
        public string ContestDescription { get; set; }
        public string Status { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
        public List<ImageReturn> Images { get; set; }
    }
}
