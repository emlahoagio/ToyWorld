namespace Entities.RequestFeatures
{
    public class NewBillParameters
    {
        public string ToyOfSellerName { get; set; }
        public string ToyOfBuyerName { get; set; }
        public bool IsExchangeByMoney { get; set; }
        public double? ExchangeValue { get; set; }
        public int BuyerId { get; set; }
        public int TradingPostId { get; set; }
    }
}
