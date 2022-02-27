using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IPostOfContestRepository
    {
        void Create(PostOfContest postOfContest);
    }
}
