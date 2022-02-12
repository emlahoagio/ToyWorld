using Contracts.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Repository
{
    public class PrizeRepository : RepositoryBase<Prize>, IPrizeRepository
    {
        public PrizeRepository(DataContext context) : base(context)
        {
        }

        public void CreatePrize(Prize prize) => Create(prize);

        public void UpdatePrize(Prize prize) => Update(prize);
    }
}
