using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Proposal
    {
        public Proposal()
        {
            Contests = new HashSet<Contest>();
            Images = new HashSet<Image>();
            ProposalPrizes = new HashSet<ProposalPrize>();
        }

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
        public bool? IsWaiting { get; set; }
        public bool? Status { get; set; }
        public int? AccountId { get; set; }
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Account Account { get; set; }
        public virtual Brand Brand { get; set; }
        public virtual Type Type { get; set; }
        public virtual ICollection<Contest> Contests { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<ProposalPrize> ProposalPrizes { get; set; }
    }
}
