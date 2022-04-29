namespace Entities.Models
{
    public partial class ReactTradingPost
    {
        public int AccountId { get; set; }
        public int TradingPostId { get; set; }

        public virtual Account Account { get; set; }
        public virtual TradingPost TradingPost { get; set; }
    }
}
