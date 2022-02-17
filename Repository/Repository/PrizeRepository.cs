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

namespace Repository.Repository
{
    public class PrizeRepository : RepositoryBase<Prize>, IPrizeRepository
    {
        public PrizeRepository(DataContext context) : base(context)
        {
        }

        public void CreatePrize(Prize prize) => Create(prize);

        public async Task<Pagination<PrizeOfContest>> GetPrize(PagingParameters paging, bool trackChanges)
        {
            var prizes = await FindAll(trackChanges).ToListAsync();

            var count = prizes.Count;

            var result = new Pagination<PrizeOfContest>
            {
                Count = count,
                Data = prizes
                .Skip((paging.PageNumber - 1) * paging.PageSize)
                .Take(paging.PageSize)
                .Select(x => new PrizeOfContest
                {
                    Description = x.Description,
                    Id = x.Id,
                    Images = x.Images.Select(y => new ImageReturn
                    {
                        Id = y.Id,
                        Url = y.Url
                    }).ToList(),
                    Name = x.Name,
                    Value = x.Value
                }).ToList(),
                PageNumber = paging.PageNumber,
                PageSize = paging.PageSize
            };

            return result;
        }

        public void UpdatePrize(Prize prize) => Update(prize);
    }
}
