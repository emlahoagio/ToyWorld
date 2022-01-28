﻿using Entities.DataTransferObject;
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
        void CreateTradingPost(NewTradingPostParameters tradingPost, int group_id, int account_id, int toy_id);
        void ExchangedTradingPost(TradingPost tradingPost);
        void UpdateTradingPost(UpdateTradingPostParameters update_infor, TradingPost tradingPost);
        Task<Pagination<TradingPostInList>> GetList(PagingParameters paging, bool trackChanges);
        Task<TradingPost> GetTradingPostById(int tradingpost_id, bool trackChanges);
        Task<UpdateTradingPost> GetUpdateDetail(int tradingpost_id, bool trackChanges);
    }
}
