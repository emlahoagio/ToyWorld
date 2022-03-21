﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DataTransferObject
{
    public class BillDetail
    {
        public int Id { get; set; }
        public string ToyOfSellerName { get; set; }
        public string ToyOfBuyerName { get; set; }
        public bool IsExchangeByMoney { get; set; }
        public double? ExchangeValue { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
    }
}