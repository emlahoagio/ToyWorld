using Contracts;
using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class ToyRepository : RepositoryBase<Toy>, IToyRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IRepositoryManager _repositoryManager;

        public ToyRepository(DataContext context, IConfiguration configuration, IRepositoryManager repositoryManager) :base(context)
        {
            _configuration = configuration;
            _repositoryManager = repositoryManager;
        }

        public async Task<Pagination<ToyInList>> GetAllToys(PagingParameters toyParameters, bool trackChanges)
        {
            var toys = await FindByCondition(x => x.Id != 3,trackChanges)
                .Include(toy => toy.Brand)
                .Include(toy => toy.Type).ToListAsync();

            int count = toys.Count();

            var pagingToys = toys.Skip((toyParameters.PageNumber - 1) * toyParameters.PageSize)
                .Take(toyParameters.PageSize)
                .OrderBy(x => x.Name);

            var toysInList = pagingToys.Select(toy => new ToyInList
            {
                Id = toy.Id,
                Name = toy.Name,
                Price = toy.Price,
                BrandName = toy.Brand.Name,
                TypeName = toy.Type.Name,
                CoverImage = toy.CoverImage
            });

            var result = new Pagination<ToyInList>
            {
                Count = count,
                Data = toysInList,
                PageNumber = toyParameters.PageNumber,
                PageSize = toyParameters.PageSize
            };

            return result;
        }

        public void CreateToy(Toy toy) => Create(toy);

        public void UpdateToy(Toy toy) => Update(toy);

        public async Task<Toy> GetExistToy(string toyName)
        {
            var toy = await FindByCondition(x => x.Name == toyName.Trim(), trackChanges: false)
                .Include(x => x.Images)
                .FirstOrDefaultAsync();
            if (toy == null)
            {
                return null;
            }
            return toy;
        }

        public async Task<Pagination<ToyInList>> GetToysByType(PagingParameters toyParameters, string typeName, bool trackChanges)
        {
            var toys = await FindByCondition(x => x.Type.Name == typeName && x.Id != 3, trackChanges)
                .Include(x => x.Type)
                .Include(x => x.Brand)
                .ToListAsync();

            int count = toys.Count();

            var pagingToys = toys.Skip((toyParameters.PageNumber - 1) * toyParameters.PageSize)
                                .Take(toyParameters.PageSize).OrderBy(x => x.Name);
                

            if (toys == null) return null;

            var toysInList = pagingToys.Select(toy => new ToyInList
            {
                BrandName = toy.Brand.Name,
                Name = toy.Name,
                CoverImage = toy.CoverImage,
                Id = toy.Id,
                Price = toy.Price,
                TypeName = toy.Type.Name
            });

            var result = new Pagination<ToyInList>
            {
                Count = count,
                Data = toysInList,
                PageNumber = toyParameters.PageNumber,
                PageSize = toyParameters.PageSize
            };

            return result;
        }

        public async Task<ToyDetail> GetToyDetail(int toyId, bool trackChanges)
        {
            var toyDetail = await FindByCondition(x => x.Id == toyId, trackChanges)
                .Include(x => x.Brand)
                .Include(x => x.Type)
                .Include(x => x.Images)
                .FirstOrDefaultAsync();

            if(toyDetail == null)
            {
                return null;
            }

            var listImages = toyDetail.Images.Select(image => new ImageReturn
            {
                Id = image.Id,
                Url = image.Url
            }).ToList();

            var result = new ToyDetail
            {
                BrandName = toyDetail.Brand.Name,
                Name = toyDetail.Name,
                CoverImage = toyDetail.CoverImage,
                Description = toyDetail.Description,
                Id = toyDetail.Id,
                ImagesOfToy = listImages,
                Price = toyDetail.Price,
                TypeName = toyDetail.Type.Name
            };

            return result;
        }

        public async Task<Toy> GetToyByName(string toyName, bool trackChanges)
        {
            var result = await FindByCondition(x => x.Name == toyName, trackChanges).FirstOrDefaultAsync();

            if (result == null) return null;

            return result;
        }

        public async Task<List<string>> GetNameOfToy(bool trackChanges)
        {
            var result = await FindAll(trackChanges).Select(x => x.Name).ToListAsync();

            return result;
        }
    }
}
