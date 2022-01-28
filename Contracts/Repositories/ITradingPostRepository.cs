using Entities.DataTransferObject;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ITradingPostRepository
    {
        void CreateTradingPost(NewTradingPost tradingPost, int group_id, int account_id, int toy_id); 
        Task<Pagination<TradingPostInList>> GetList(PagingParameters paging, bool trackChanges);
    }
}
