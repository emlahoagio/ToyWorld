using Entities.DataTransferObject;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IReactTradingPostRepository
    {
        void Create(ReactTradingPost react);
        void Delete(ReactTradingPost react);
        Task<ReactTradingPost> FindReact(int trading_post_id, int account_id, bool trackChanges);
        Task<Pagination<TradingManaged>> GetIsReactedReactTrading(Pagination<TradingManaged> result, int account_id, bool trackChanges);
        Task<Pagination<TradingPostInList>> GetIsReactedReactTrading(Pagination<TradingPostInList> result, int account_id, bool trackChanges);
    }
}
