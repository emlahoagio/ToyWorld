using Contracts.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class PostOfContestRepository : RepositoryBase<PostOfContest>, IPostOfContestRepository
    {
        public PostOfContestRepository(DataContext context) : base(context)
        {
        }
    }
}
