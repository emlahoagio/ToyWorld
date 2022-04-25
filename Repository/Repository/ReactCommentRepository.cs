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
    public class ReactCommentRepository : RepositoryBase<ReactComment>, IReactCommentRepository
    {
        public ReactCommentRepository(DataContext context) : base(context)
        {
        }

        public void CreateReact(ReactComment reactComment) => Create(reactComment);

        public void DeleteReact(ReactComment reactComment) => Delete(reactComment);

        public async Task<List<AccountReact>> GetAccountReactComment(int comment_id, bool trackChanges)
        {
            var reactsComment = await FindByCondition(x => x.CommentId == comment_id, trackChanges)
                .Include(x => x.Account)
                .ToListAsync();

            if (reactsComment == null) return null;

            var result = reactsComment.Select(x => new AccountReact
            {
                Avatar = x.Account.Avatar,
                Id = x.Account.Id,
                Name = x.Account.Name
            }).ToList();

            return result;
        }

        public async Task<int> GetNumOfReact(int comment_id, bool trackChanges)
        {
            var reacts = await FindByCondition(x => x.CommentId == comment_id, trackChanges).CountAsync();

            return reacts;
        }
    }
}
