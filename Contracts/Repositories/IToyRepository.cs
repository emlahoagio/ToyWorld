using Entities.DataTransferObject;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IToyRepository
    {
        Task<Pagination<ToyInList>> GetAllToys(PagingParameters toyParameters, bool trackChanges);
        Task<Pagination<ToyInList>> GetToysByType(PagingParameters toyParameters, string typeName, bool trackChanges);
        Task<ToyDetail> GetToyDetail(int toyId, bool trackChanges);
        Task<Toy> GetToyByName(string toyName, bool trackChanges);
        Task<List<string>> GetNameOfToy(bool trackChanges);
        void CreateToy(Toy toy);
        void UpdateToy(Toy toy);
        Task<Toy> GetExistToy(string toyName);
    }
}
