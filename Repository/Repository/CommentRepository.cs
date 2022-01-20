using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }
        public void CreateComment(Comment comment) => Create(comment);

        public async Task<Comment> GetCommentById(int comment_id, bool trackChanges)
        {
            var comment = await FindByCondition(x => x.Id == comment_id, trackChanges)
                .Include(x => x.ReactComments)
                .FirstOrDefaultAsync();

            if (comment == null) return null;

            return comment;
        }

        public bool IsReactedComment(Comment comment, int accountId)
        {
            bool result = false;
            foreach(var reactComment in comment.ReactComments)
            {
                if(reactComment.AccountId == accountId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
