using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IBillRepository
    {
        void Create(Bill bill);
        void UpdateBillStatus(Bill bill, int update_status);
        Task<Bill> GetBillById(int bill_id, bool trackChanges);
        void AcceptBill(Bill bill);
        void DenyBill(Bill bill);
        Task<int> GetIdOfCreatedBill(DateTime findTime, bool trackChanges);
        Task<BillDetail> GetBillDetail(int bill_id, bool trackChanges);
        Task<List<BillInList>> GetBillByTradingPost(int trading_id, bool trackChanges);
    }
}
