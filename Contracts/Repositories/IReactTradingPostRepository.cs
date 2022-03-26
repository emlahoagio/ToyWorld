using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IReactTradingPostRepository
    {
        void Create(ReactTradingPost react);
        void Delete(ReactTradingPost react);
        Task<ReactTradingPost> FindReact(int trading_post_id, int account_id, bool trackChanges);
    }
}
