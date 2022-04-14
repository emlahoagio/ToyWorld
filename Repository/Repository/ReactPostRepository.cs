using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ReactPostRepository : RepositoryBase<ReactPost>, IReactPostRepository
    {
        public ReactPostRepository(DataContext context) : base(context)
        {
        }

        public void CreateReact(ReactPost reactPost) => Create(reactPost);

        public void DeleteReact(ReactPost reactPost) => Delete(reactPost);

        public async Task<List<AccountReact>> GetAccountReactPost(int post_id, bool trackChanges)
        {
            var reactPosts = await FindByCondition(x => x.PostId == post_id, trackChanges)
                .Include(x => x.Account)
                .ToListAsync();

            if (reactPosts == null) return null;

            var result = reactPosts.Select(x => new AccountReact
            {
                Avatar = x.Account.Avatar,
                Id = x.Account.Id,
                Name = x.Account.Name
            }).ToList();
            
            return result;
        }

        public async Task<Pagination<PostInList>> GetReactForPost(Pagination<PostInList> result, int account_id, bool trackChanges)
        {
            var data = new List<PostInList>();

            foreach(var post in result.Data)
            {
                var reacts = await FindByCondition(x => x.PostId == post.Id, trackChanges).ToListAsync();

                post.NumOfReact = reacts.Count();
                post.IsLikedPost = reacts.Where(x => x.AccountId == account_id) != null;

                data.Add(post);
            }

            result.Data = data;

            return result;
        }
    }
}
