using Contracts.Repositories;
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

        public async Task<Pagination<PostInList>> GetPostByGroupId(int groupId, bool trackChanges, PagingParameters paging)
        {
            var listPost = await FindByCondition(post => post.GroupId == groupId, trackChanges)
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
