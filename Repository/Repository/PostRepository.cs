﻿using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(DataContext context) : base(context)
        {
        }

        public void ApprovePost(Post post)
        {
            post.IsPublic = true;
            post.IsWaiting = false;
            post.PublicDate = DateTime.Now;
            Update(post);
        }

        public void DenyPost(Post post)
        {
            post.IsWaiting = false;
            Update(post);
        }

        public void CreatePost(NewPostParameter param, int accountId)
        {
            var post = new Post
            {
                AccountId = accountId,
                Content = param.Content,
                GroupId = param.GroupId,
                ToyId = param.ToyId == 0 ? 3 : param.ToyId,
                Images = param.ImagesLink.Select(x => new Image { Url = x }).ToList(),
                IsWaiting = true,
                IsPublic = false,
                IsDeleted = false,
                PostDate = DateTime.Now
            };
            Create(post);
        }

        public async Task<Pagination<PostInList>> GetPostByGroupId(int groupId, bool trackChanges, PagingParameters paging)
        {
            var listPost = await FindByCondition(post => post.GroupId == groupId && post.IsPublic == true && post.IsDeleted == false, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.Images)
                .Include(x => x.ReactPosts)
                .Include(x => x.Comments)
                .OrderByDescending(x => x.PostDate)
                .ToListAsync();

            var count = listPost.Count();

            var pagingList = listPost.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            if(count == 0 || listPost == null)
            {
                return null;
            }

            var result = listPost.Select(x => new PostInList
            {
                Id = x.Id,
                Images = x.Images.Select(x => new ImageReturn
                {
                    Id = x.Id,
                    Url = x.Url
                }).ToList(),
                NumOfComment = x.Comments.Count,
                NumOfReact = x.ReactPosts.Count,
                Content = x.Content,
                OwnerAvatar = x.Account.Avatar,
                OwnerName = x.Account.Name,
                PublicDate = x.PublicDate
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
            var result = await FindByCondition(x => x.Id == post_id && x.IsPublic == true && x.IsDeleted == false, trackChanges)
                .Include(x => x.ReactPosts)
                .FirstOrDefaultAsync();

            if (result == null) return null;

            return result;
        }

        public async Task<PostDetail> GetPostDetail(int post_id, bool trackChanges)
        {
            var post = await FindByCondition(x => x.Id == post_id && x.IsPublic == true && x.IsDeleted == false, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.Images)
                .Include(x => x.Comments).ThenInclude(x => x.ReactComments)
                .Include(x => x.Comments).ThenInclude(x => x.Account)
                .Include(x => x.ReactPosts)
                .FirstOrDefaultAsync();

            if (post == null) return null;

            var result = new PostDetail
            {
                Id = post.Id,
                NumOfComment = post.Comments.Count,
                Comments = post.Comments.Select(x => new CommentReturn
                {
                    Id = x.Id,
                    Content = x.Content,
                    NumOfReact = x.ReactComments.Count,
                    OwnerAvatar = x.Account.Avatar,
                    OwnerName = x.Account.Name
                }).ToList(),
                Images = post.Images.Select(x => new ImageReturn
                {
                    Id = x.Id,
                    Url = x.Url
                }).ToList(),
                NumOfReact = post.ReactPosts.Count,
                OwnerAvatar = post.Account.Avatar,
                OwnerName = post.Account.Name,
                PublicDate = post.PublicDate,
                Content = post.Content
            };

            return result;
        }

        public async Task<Pagination<WaitingPost>> GetWaitingPost(bool trackChanges, PagingParameters param)
        {
            var posts = await FindByCondition(x => x.IsWaiting == true, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.Images)
                .Include(x => x.ReactPosts)
                .Include(x => x.Comments)
                .OrderByDescending(x => x.PostDate)
                .ToListAsync();

            int count = posts.Count;

            var pagingPosts = posts.Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize);

            if (posts == null || posts.Count == 0) return null;

            var waitingPosts = pagingPosts.Select(x => new WaitingPost
            {
                Content = x.Content,
                Id = x.Id,
                Images = x.Images.Select(x => new ImageReturn
                {
                    Id = x.Id,
                    Url = x.Url
                }).ToList(),
                OwnerAvatar = x.Account.Avatar,
                OwnerName = x.Account.Name,
                PostDate = x.PostDate
            });

            var result = new Pagination<WaitingPost>
            {
                Count = count,
                Data = waitingPosts,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };

            return result;
        }

        public async Task<Pagination<WaitingPost>> GetWaitingPost(bool trackChanges, PagingParameters param, int accountId)
        {
            var posts = await FindByCondition(x => x.IsWaiting == true && x.AccountId == accountId, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.Images)
                .Include(x => x.ReactPosts)
                .Include(x => x.Comments)
                .OrderByDescending(x => x.PostDate)
                .ToListAsync();

            int count = posts.Count;

            var pagingPosts = posts.Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize);

            if (posts == null || posts.Count == 0) return null;

            var waitingPosts = pagingPosts.Select(x => new WaitingPost
            {
                Content = x.Content,
                Id = x.Id,
                Images = x.Images.Select(x => new ImageReturn
                {
                    Id = x.Id,
                    Url = x.Url
                }).ToList(),
                OwnerAvatar = x.Account.Avatar,
                OwnerName = x.Account.Name,
                PostDate = x.PostDate
            });

            var result = new Pagination<WaitingPost>
            {
                Count = count,
                Data = waitingPosts,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
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

        public async Task<Post> GetPostApproveOrDenyById(int post_id, bool trackChanges)
        {
            var post = await FindByCondition(x => x.Id == post_id && x.IsWaiting == true, trackChanges).FirstOrDefaultAsync();

            if (post == null) return null;

            return post;
        }

        public async Task<Post> GetDisablePost(int post_id, bool trackChanges)
        {
            var post = await FindByCondition(x => x.Id == post_id, trackChanges).FirstOrDefaultAsync();

            if (post == null) return null;

            return post;
        }

        public void DisablePost(Post post)
        {
            post.IsDeleted = true;
            Update(post);
        }

        public async Task<Pagination<PostInList>> GetPostByAccountId(int accountId, bool trackChanges, PagingParameters paging)
        {
            var listPost = await FindByCondition(post => post.AccountId == accountId && post.IsPublic == true && post.IsDeleted == false, trackChanges)
                .Include(x => x.Account)
                .Include(x => x.Images)
                .Include(x => x.ReactPosts)
                .Include(x => x.Comments)
                .OrderByDescending(x => x.PostDate)
                .ToListAsync();

            var count = listPost.Count();

            var pagingList = listPost.Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize);

            if (count == 0 || listPost == null)
            {
                return null;
            }

            var result = listPost.Select(x => new PostInList
            {
                Id = x.Id,
                Images = x.Images.Select(x => new ImageReturn
                {
                    Id = x.Id,
                    Url = x.Url
                }).ToList(),
                NumOfComment = x.Comments.Count,
                NumOfReact = x.ReactPosts.Count,
                Content = x.Content,
                OwnerAvatar = x.Account.Avatar,
                OwnerName = x.Account.Name,
                PublicDate = x.PublicDate
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
    }
}
