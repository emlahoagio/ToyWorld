using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Type
    {
        public Type()
        {
            Contests = new HashSet<Contest>();
            Proposals = new HashSet<Proposal>();
            Toys = new HashSet<Toy>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Contest> Contests { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
        public virtual ICollection<Toy> Toys { get; set; }
    }
}
