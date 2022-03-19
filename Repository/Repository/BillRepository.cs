using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public async Task<Bill> GetAcceptOrDeny(int bill_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.Id == bill_id, trackChanges).FirstOrDefaultAsync();

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
    }
}
