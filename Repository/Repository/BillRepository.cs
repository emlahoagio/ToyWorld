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
            bill.UpdateTime = DateTime.UtcNow.AddHours(7);
            Update(bill);
        }

        public void DenyBill(Bill bill)
        {
            bill.Status = 3;
            bill.UpdateTime = DateTime.UtcNow.AddHours(7);
            Update(bill);
        }

        public async Task<Bill> GetBillById(int bill_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.Id == bill_id, trackChanges).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Pagination<BillByStatus>> GetBillByStatus(int status, PagingParameters paging, bool trackChanges)
        {
            var bills = await FindByCondition(x => x.Status == status, trackChanges)
                .Skip((paging.PageNumber - 1) * paging.PageSize).Take(paging.PageSize)
                .Include(x => x.Buyer)
                .Include(x => x.Seller)
                .Include(x => x.TradingPost)
                .OrderByDescending(x => x.UpdateTime)
                .ToListAsync();

            var count = await FindByCondition(x => x.Status == status, trackChanges).CountAsync();

            var data_result = bills.Select(x => new BillByStatus
            {
                UpdateTime = x.UpdateTime,
                Id = x.Id,
                IdPost = x.TradingPostId,
                PostTitle = x.TradingPost.Title,
                ReceiverName = x.Buyer.Name,
                ReceiverToy = x.ToyOfBuyerName,
                SenderName = x.Seller.Name,
                SenderToy = x.ToyOfSellerName,
                Status = x.Status
            }).ToList();

            var result = new Pagination<BillByStatus>
            {
                Count = count,
                Data = data_result,
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

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
                UpdateTime = x.UpdateTime,
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
                UpdateTime = bill.UpdateTime,
                ExchangeValue = bill.ExchangeValue,
                Id = bill.Id,
                IsExchangeByMoney = bill.IsExchangeByMoney,
                SellerName = bill.Seller.Name,
                Status = bill.Status,
                ToyOfBuyerName = bill.ToyOfBuyerName,
                ToyOfSellerName = bill.ToyOfSellerName,
                BuyerId = bill.BuyerId,
                SellerId = bill.SellerId
            };

            return result;
        }

        public async Task<RateAccount> GetDataForRate(int account_id, RateAccount rateOfAccount, bool trackChanges)
        {
            var listRate = new List<RateSellerReturn>();

            foreach(var rate in rateOfAccount.Rates)
            {
                var bill = await FindByCondition(x => x.SellerId == account_id && x.BuyerId == rate.RateOwnerId && x.Status == 2, trackChanges)
                    .Include(x => x.TradingPost)
                    .FirstOrDefaultAsync();

                rate.TradingPostId = bill.TradingPostId;
                rate.TradingPostTitle = bill.TradingPost.Title;

                listRate.Add(rate);
            }
            rateOfAccount.Rates = listRate;
            return rateOfAccount;
        }

        public async Task<int> GetIdOfCreatedBill(DateTime findTime, bool trackChanges)
        {
            var result = await FindByCondition(x => x.UpdateTime == findTime, trackChanges).FirstOrDefaultAsync();

            return result.Id;
        }

        public void UpdateBillStatus(Bill bill, int update_status)
        {
            bill.Status = update_status;
            bill.UpdateTime = DateTime.UtcNow.AddHours(7);
            Update(bill);
        }
    }
}
