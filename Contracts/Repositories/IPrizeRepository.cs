using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPrizeRepository
    {
        Task<Pagination<PrizeOfContest>> GetPrize(PagingParameters paging, bool trackChanges);
        void CreatePrize(Prize prize);
        void UpdatePrize(Prize prize);
        Task UpdatePrize(EditPrizeParameters param, int prize_id, bool trackChanges);
        Task<PrizeReturn> GetUpdatePrize(int prize_id, bool trackChanges);
        Task DisablePrize(int prize_id, bool trackChanges);
    }
}
