using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class RateSellerRepository : RepositoryBase<RateSeller>, IRateSellerRepository
    {
        public RateSellerRepository(DataContext context) : base(context)
        {
        }

        public async Task<RateAccount> GetRateOfAccount(int account_id, bool trackChanges)
        {
            var rates = await FindByCondition(x => x.SellerId == account_id, trackChanges)
                .Include(x => x.Buyer)
                .ToListAsync();

            if (rates == null) return null;

            var average = rates.Select(x => x.NumOfStar).Average();

            var result = new RateAccount
            {
                AverageStar = average,
                Rates = rates.Select(x => new RateSellerReturn
                {
                    Content = x.Content,
                    NumOfStar = x.NumOfStar,
                    RateOwnerAvatar = x.Buyer.Avatar,
                    RateOwnerId = x.Buyer.Id,
                    RateOwnerName = x.Buyer.Name
                }).ToList()
            };

            return result;
        }

        public async Task<bool> IsRated(int sellerId, int buyerId, bool trackChanges)
        {
            var result = await FindByCondition(x => x.SellerId == sellerId && x.BuyerId == buyerId, trackChanges)
                .FirstOrDefaultAsync() != null;

            return result;
        }

        public void NewRateSeller(RateSeller rateSeller) => Create(rateSeller);
    }
}
