using System;
using System.Collections.Generic;

namespace Entities.DataTransferObject
{
    public class TradingPostDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ToyName { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string Trading { get; set; }
        public decimal? Value { get; set; }
        public string Phone { get; set; }
        public DateTime PostDate { get; set; }
        public int Status { get; set; }
        public int GroupId { get; set; }
        public int? OwnerId { get; set; }
        public string OwnerName { get; set; }
        public string OwnerAvatar { get; set; }
        public int? ToyId { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
        public bool IsReact { get; set; }
        public int NumOfReact { get; set; }
    }
}
