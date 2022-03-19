using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class TradingPostInList
    {
        public int Id { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerAvatar { get; set; }
        public string OwnerName { get; set; }
        public DateTime PostDate { get; set; }
        public string ToyName { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Address { get; set; }
        public string Exchange { get; set; }
        public string Content { get; set; }
        public decimal? Value { get; set; }
        public List<ImageReturn> Images { get; set; }
        public int NoOfReact { get; set; }
        public int NoOfComment { get; set; }
        public bool IsLikedPost { get; set; }
    }
}
