#nullable disable

namespace Entities.Models
{
    public partial class PrizeContest
    {
        public int ContestId { get; set; }
        public int PrizeId { get; set; }

        public virtual Contest Contest { get; set; }
        public virtual Prize Prize { get; set; }
    }
}
