using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface ITradingPostRepository
    {
        void CreateTradingPost(NewTradingPost tradingPost, int group_id, int account_id, int toy_id); 
        void CreateTradingPost(NewTradingPost tradingPost, int group_id, int account_id);
    }
}
