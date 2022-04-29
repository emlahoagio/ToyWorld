using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Pagination<TradingManaged>> GetIsReactedReactTrading(Pagination<TradingManaged> result, int account_id, bool trackChanges)
        {
            var data_result = new List<TradingManaged>();

            foreach(var trading in result.Data)
            {
                var isLiked = await FindByCondition(x => x.TradingPostId == trading.Id && x.AccountId == account_id, trackChanges).FirstOrDefaultAsync() != null;

                trading.IsLikedPost = isLiked;
                data_result.Add(trading);
            }
            result.Data = data_result;

            return result;
        }

        public async Task<Pagination<TradingPostInList>> GetIsReactedReactTrading(Pagination<TradingPostInList> result, int account_id, bool trackChanges)
        {
            var data_result = new List<TradingPostInList>();

            foreach (var trading in result.Data)
            {
                var reacts = await FindByCondition(x => x.TradingPostId == trading.Id, trackChanges).ToListAsync();

                trading.IsLikedPost = reacts.Where(x => x.AccountId == account_id).Count() > 0;
                trading.NoOfReact = reacts.Count();

                data_result.Add(trading);
            }
            result.Data = data_result;

            return result;
        }
    }
}
