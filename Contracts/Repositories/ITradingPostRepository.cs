using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ITradingPostRepository
    {
        void CreateTradingPost(NewTradingPostParameters tradingPost, int group_id, int account_id, int toy_id, int brand_id, int type_id);
        void ExchangedTradingPost(TradingPost tradingPost);
        void UpdateTradingPost(UpdateTradingPostParameters update_infor, TradingPost tradingPost);
        void DisableOrEnable(TradingPost tradingPost, int disable_or_enable);
        Task UpdateTradingStatus(int trading_post_id, int trading_status , bool trackChanges);
        Task<Pagination<TradingPostInList>> GetTradingPostInGroup(int group_id, PagingParameters paging, bool trackChanges, int account_id);
        Task<TradingPost> GetTradingPostById(int tradingpost_id, bool trackChanges);
        Task<UpdateTradingPost> GetUpdateDetail(int tradingpost_id, bool trackChanges);
    }
}
