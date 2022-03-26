using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ReactTradingPostRepository : RepositoryBase<ReactTradingPost>, IReactTradingPostRepository
    {
        public ReactTradingPostRepository(DataContext context) : base(context)
        {
        }

        public async Task<ReactTradingPost> FindReact(int trading_post_id, int account_id, bool trackChanges)
        {
            var react = await FindByCondition(x => x.AccountId == account_id && x.TradingPostId == trading_post_id, trackChanges)
                .FirstOrDefaultAsync();

            return react;
        }
    }
}
