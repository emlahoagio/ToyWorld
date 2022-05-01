using System.ComponentModel.DataAnnotations;

namespace Entities.RequestFeatures
{
    public class NewBillParameters
    {
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string ToyOfSellerName { get; set; }
        [Required]
        [MinLength(6)]
        [MaxLength(256)]
        public string ToyOfBuyerName { get; set; }
        [Required]
        public bool IsExchangeByMoney { get; set; }
        [Required]
        [MaxLength(11)]
        public double? ExchangeValue { get; set; }
        [Required]
        public int BuyerId { get; set; }
        [Required]
        public int TradingPostId { get; set; }
    }
}
