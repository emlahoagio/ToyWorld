using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewBillParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string ToyOfSellerName { get; set; }
        public string ToyOfBuyerName { get; set; }
        public bool IsExchangeByMoney { get; set; }
        public double? ExchangeValue { get; set; }
        [Required]
        public int BuyerId { get; set; }
        [Required]
        public int TradingPostId { get; set; }
    }
}
