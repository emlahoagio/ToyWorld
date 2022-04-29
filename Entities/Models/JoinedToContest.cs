namespace Entities.Models
{
    public partial class JoinedToContest
    {
        public int AccountId { get; set; }
        public int ContestId { get; set; }
        public bool IsBan { get; set; }

        public virtual Account Account { get; set; }
        public virtual Contest Contest { get; set; }
    }
}
