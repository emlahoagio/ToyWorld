using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class ProposalInList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? MinRegister { get; set; }
        public int? MaxRegister { get; set; }
        public string Description { get; set; }
        public int? Duration { get; set; }
        public string Location { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAvatar { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
    }
}
