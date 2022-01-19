using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPostRepository
    {
        Task<Pagination<PostInList>> GetPostByGroupId(int groupId, bool trackChanges, PagingParameters paging);

        Task<PostDetail> GetPostDetail(int post_id, bool trackChanges);

        Task<Post> GetPostById(int post_id, bool trackChanges);

        bool IsReactedPost(Post post, int account_id);
    }
}
