using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class ProposalInList
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public int GroupId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rule { get; set; }
        public string Slogan { get; set; }
        public int OwnerId { get; set; }
        public string OwnerName { get; set; }
    }
}
