using Contracts.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class EvaluateContestRepository : RepositoryBase<Evaluate>, IEvaluateContestRepository
    {
        public EvaluateContestRepository(DataContext context) : base(context)
        {
        }
    }
}
