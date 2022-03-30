using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(DataContext context) : base(context)
        {
        }

        public void AcceptBill(Bill bill)
        {
            bill.Status = 1;
            Update(bill);
        }

        public void DenyBill(Bill bill)
        {
            bill.Status = 3;
            Update(bill);
        }

        public async Task<Bill> GetBillById(int bill_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.Id == bill_id, trackChanges).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<BillInList>> GetBillByTradingPost(int trading_id, bool trackChanges)
        {
            var bills = await FindByCondition(x => x.TradingPostId == trading_id, trackChanges)
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .ToListAsync();

            var result = bills.Select(x => new BillInList
            {
                BuyerId = x.BuyerId,
                BuyerName = x.Buyer.Name,
                CreateTime = x.CreateTime,
                ExchangeValue = x.ExchangeValue,
                Id = x.Id,
                IsExchangeByMoney = x.IsExchangeByMoney,
                SellerId = x.SellerId,
                SellerName = x.Seller.Name,
                Status = x.Status,
                ToyOfBuyerName = x.ToyOfBuyerName,
                ToyOfSellerName = x.ToyOfSellerName,
                TradingPostId = x.TradingPostId
            }).ToList();

            return result;
        }

        public async Task<BillDetail> GetBillDetail(int bill_id, bool trackChanges)
        {
            var bill = await FindByCondition(x => x.Id == bill_id, trackChanges)
                .Include(x => x.Seller)
                .Include(x => x.Buyer)
                .FirstOrDefaultAsync();

            var result = new BillDetail
            {
                BuyerName = bill.Buyer.Name,
                CreateTime = bill.CreateTime,
                ExchangeValue = bill.ExchangeValue,
                Id = bill.Id,
                IsExchangeByMoney = bill.IsExchangeByMoney,
                SellerName = bill.Seller.Name,
                Status = bill.Status,
                ToyOfBuyerName = bill.ToyOfBuyerName,
                ToyOfSellerName = bill.ToyOfSellerName
            };

            return result;
        }

        public async Task<int> GetIdOfCreatedBill(DateTime findTime, bool trackChanges)
        {
            var result = await FindByCondition(x => x.CreateTime == findTime, trackChanges).FirstOrDefaultAsync();

            return result.Id;
        }

        public void UpdateBillStatus(Bill bill, int update_status)
        {
            bill.Status = update_status;
            Update(bill);
        }
    }
}
