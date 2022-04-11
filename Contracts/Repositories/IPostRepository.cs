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
        Task<Pagination<WaitingPost>> GetWaitingPost(bool trackChanges, PagingParameters param);
        Task<Pagination<WaitingPost>> GetWaitingPost(bool trackChanges, PagingParameters param, int accountId);
        Task<Pagination<PostInList>> GetPostByGroupId(int groupId, bool trackChanges, PagingParameters paging, int accountId);
        Task<Pagination<PostInList>> GetPostByAccountId(int accountId, bool trackChanges, PagingParameters paging);
        Task<PostDetail> GetPostDetail(int post_id, bool trackChanges, int account_id);
        Task<Post> GetPostReactById(int post_id, bool trackChanges);
        Task<Post> GetPostApproveOrDenyById(int post_id, bool trackChanges);
        Task<Post> GetDeletePost(int post_id, bool trackChanges);
        Task<int> GetNumOfReact(int post_id, bool trackChanges);
        bool IsReactedPost(Post post, int account_id);
        void CreatePost(NewPostParameter param, int accountId);
        void ApprovePost(Post post);
        void DenyPost(Post post);
        void Delete(Post post);

        Task<int> GetOwnerByPostId(int postId);
        Task<Pagination<PostInList>> GetPostByFavorite(PagingParameters paging, int account_id, bool trackChanges);
    }
}
