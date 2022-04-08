using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public partial class Bill
    {
        public Bill()
        {
            Images = new HashSet<Image>();
        }

        public int Id { get; set; }
        public string ToyOfSellerName { get; set; }
        public string ToyOfBuyerName { get; set; }
        public bool IsExchangeByMoney { get; set; }
        public double? ExchangeValue { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public int TradingPostId { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }

        public virtual Account Seller { get; set; }
        public virtual Account Buyer { get; set; }
        public virtual TradingPost TradingPost { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
