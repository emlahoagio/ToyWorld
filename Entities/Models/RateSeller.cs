namespace Entities.Models
{
    public partial class RateSeller
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int SellerId { get; set; }
        public double NumOfStar { get; set; }
        public string Content { get; set; }

        public Account Buyer { get; set; }
        public Account Seller { get; set; }
    }
}
