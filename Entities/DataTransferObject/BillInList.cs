using System;

namespace Entities.DataTransferObject
{
    public class BillInList
    {
        public int Id { get; set; }
        public string ToyOfSellerName { get; set; }
        public string ToyOfBuyerName { get; set; }
        public bool IsExchangeByMoney { get; set; }
        public double? ExchangeValue { get; set; }
        public int SellerId { get; set; }
        public string SellerName { get; set; }
        public int BuyerId { get; set; }
        public string BuyerName { get; set; }
        public int TradingPostId { get; set; }
        public int Status { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
