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
        Task<Pagination<ToyInList>> GetAllToys(ToyParameters toyParameters, bool trackChanges);
        Task<Pagination<ToyInList>> GetToysByType(ToyParameters toyParameters, string typeName, bool trackChanges);
        Task<ToyDetail> GetToyDetail(int toyId, bool trackChanges);
        void CreateToy(Toy toy);
        void UpdateToy(Toy toy);
        Task<Toy> GetExistToy(string toyName);
    }
}
