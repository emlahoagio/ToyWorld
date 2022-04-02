using Entities.DataTransferObject;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRateSellerRepository
    {
        void NewRateSeller(RateSeller rateSeller);
        Task<bool> IsRated(int sellerId, int buyerId, bool trackChanges);
        Task<RateAccount> GetRateOfAccount(int account_id, bool trackChanges);
    }
}
