using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }

        public void CreatePost(NewPostParameter param, int accountId)
        {
            var post = new Post
            {
                AccountId = accountId,
                Content = param.Content,
                GroupId = param.GroupId,
                Images = param.ImagesLink.Select(x => new Image { Url = x }).ToList(),
                PostDate = DateTime.Now.AddHours(7)
            };
            Create(post);
        }

        public async Task<Pagination<PostInList>> GetPostByGroupId(int groupId, bool trackChanges, PagingParameters paging, int accountId)
        {
            var listPost = await FindByCondition(post => post.GroupId == groupId, trackChanges)
                .OrderByDescending(x => x.PostDate)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Account)
                .Include(x => x.ReactPosts)
                .ToListAsync();

            var count = await FindByCondition(post => post.GroupId == groupId, trackChanges)
                .CountAsync();

            if (listPost.Count == 0)
            {
                return null;
            }

            var result = listPost.Select(x => new PostInList
            {
                Id = x.Id,
                NumOfReact = x.ReactPosts.Count,
                Content = x.Content,
                OwnerId = x.AccountId,
                OwnerAvatar = x.Account.Avatar,
                IsLikedPost = x.ReactPosts.Where(y => y.AccountId == accountId).Count() == 0 ? false : true,
                OwnerName = x.Account.Name,
                PostDate = x.PostDate.Value
            }).ToList();

            var pagingNation = new Pagination<PostInList>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = result
            };

            return pagingNation;
        }

        public async Task<Post> GetPostReactById(int post_id, bool trackChanges)
        {
            var result = await FindByCondition(x => x.Id == post_id, trackChanges)
                .Include(x => x.ReactPosts)
                .FirstOrDefaultAsync();

            if (result == null) return null;

            return result;
        }

        public async Task<PostDetail> GetPostDetail(int post_id, bool trackChanges, int account_id)
        {
            var post = await FindByCondition(x => x.Id == post_id, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.ReactPosts)
                .FirstOrDefaultAsync();

            if (post == null) return null;

            var result = new PostDetail
            {
                Id = post.Id,
                NumOfReact = post.ReactPosts.Count,
                OwnerId = post.Account.Id,
                OwnerAvatar = post.Account.Avatar,
                OwnerName = post.Account.Name,
                PostDate = post.PostDate,
                Content = post.Content,
                IsLikedPost = post.ReactPosts.Where(y => y.AccountId == account_id).Count() == 0 ? false : true
            };

            return result;
        }

        public bool IsReactedPost(Post post, int account_id)
        {
            bool result = false;
            foreach (var reactPost in post.ReactPosts)
            {
                if (reactPost.AccountId == account_id)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public async Task<Post> GetDeletePost(int post_id, bool trackChanges)
        {
            var post = await FindByCondition(x => x.Id == post_id, trackChanges)
                .Include(x => x.Images)
                .Include(x => x.Comments).ThenInclude(y => y.ReactComments)
                .Include(x => x.ReactPosts)
                .FirstOrDefaultAsync();

            if (post == null) return null;

            return post;
        }

        public async Task<Pagination<PostInList>> GetPostByAccountId(int accountId, bool trackChanges, PagingParameters paging)
        {
            var listPost = await FindByCondition(post => post.AccountId == accountId, trackChanges)
                .OrderByDescending(x => x.PostDate)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Account)
                .ToListAsync();

            var count = listPost.Count();

            if (count == 0 || listPost == null)
            {
                return null;
            }

            var result = listPost.Select(x => new PostInList
            {
                Id = x.Id,
                //NumOfReact = x.ReactPosts.Count,
                Content = x.Content,
                OwnerId = x.AccountId,
                OwnerAvatar = x.Account.Avatar,
                OwnerName = x.Account.Name,
                PostDate = x.PostDate.Value,
            }).ToList();

            var pagingNation = new Pagination<PostInList>
            {
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber,
                Count = count,
                Data = result
            };

            return pagingNation;
        }

        public async Task<int> GetNumOfReact(int post_id, bool trackChanges)
        {
            var post = await FindByCondition(x => x.Id == post_id, trackChanges)
                .Include(x => x.ReactPosts)
                .FirstOrDefaultAsync();

            if (post == null)
                return 0;

            return post.ReactPosts.Count;
        }

        public async Task<int> GetOwnerByPostId(int postId)
        {
            var post = await FindByCondition(x => x.Id == postId, false).FirstOrDefaultAsync();
            return (int)post.AccountId;
        }

        public async Task<Pagination<PostInList>> GetPostFollowedGroup(List<int> groupids, PagingParameters paging, int account_id, bool trackChanges)
        {


            var posts = await FindByCondition(x => x.PostDate >= DateTime.UtcNow.AddMonths(-1) && groupids.Contains(x.GroupId.Value), trackChanges)
                .OrderByDescending(x => x.PostDate)
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Include(x => x.Account)
                .Include(x => x.ReactPosts)
                .ToListAsync();

            return new Pagination<PostInList>
            {
                Count = await FindByCondition(x => x.PostDate >= DateTime.UtcNow.AddMonths(-1) && groupids.Contains(x.GroupId.Value), trackChanges).CountAsync(),
                Data = posts.Select(x => new PostInList
                {
                    Id = x.Id,
                    NumOfReact = x.ReactPosts.Count,
                    Content = x.Content,
                    OwnerId = x.AccountId,
                    OwnerAvatar = x.Account.Avatar,
                    IsLikedPost = x.ReactPosts.Where(y => y.AccountId == account_id).Count() == 0 ? false : true,
                    OwnerName = x.Account.Name,
                    PostDate = x.PostDate.Value
                }).ToList(),
                PageSize = paging.PageSize,
                PageNumber = paging.PageNumber
            };

        }
    }
}
