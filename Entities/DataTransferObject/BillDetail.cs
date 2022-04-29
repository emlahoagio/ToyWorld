using System;
using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class BillDetail
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
        public int Status { get; set; }
        public DateTime UpdateTime { get; set; }
        public bool IsRated { get; set; }
        public List<ImageReturn> Images { get; set; }
    }
}
