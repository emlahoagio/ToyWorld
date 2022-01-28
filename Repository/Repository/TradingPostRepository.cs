using Contracts.Repositories;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository.Repository
{
    public class TradingPostRepository : RepositoryBase<TradingPost>, ITradingPostRepository
    {
        public TradingPostRepository(DataContext context) : base(context)
        {
        }

        public void CreateTradingPost(NewTradingPost tradingPost, int group_id, int account_id)
        {
            var newTradingPost = new TradingPost
            {
                AccountId = account_id,
                Address = tradingPost.Address,
                Content = tradingPost.Content,
                Phone = tradingPost.Phone,
                Title = tradingPost.Title,
                ToyName = tradingPost.ToyName,
                Trading = tradingPost.Exchange,
                Value = tradingPost.Value,
                Images = tradingPost.ImagesLink.Select(x => new Image { Url = x }).ToList()
            };

            Create(newTradingPost);
        }

        public void CreateTradingPost(NewTradingPost tradingPost, int group_id, int account_id, int toy_id)
        {
            var newTradingPost = new TradingPost
            {
                AccountId = account_id,
                Address = tradingPost.Address,
                Content = tradingPost.Content,
                Phone = tradingPost.Phone,
                Title = tradingPost.Title,
                ToyName = tradingPost.ToyName,
                Trading = tradingPost.Exchange,
                Value = tradingPost.Value,
                ToyId = toy_id,
                Images = tradingPost.ImagesLink.Select(x => new Image { Url = x }).ToList()
            };

            Create(newTradingPost);
        }
    }
}
