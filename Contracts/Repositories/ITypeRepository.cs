using Entities.DataTransferObject;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ITypeRepository
    {
        Task<Entities.Models.Type> GetTypeByName(string name, bool trackChanges);
        void CreateType(Entities.Models.Type type);
        Task<IEnumerable<string>> GetListName(bool trackChanges);
        Task<List<string>> GetTypeCreateContest(bool trackChanges);
        Task<List<TypeInList>> GetTypeToAddFavorite(int accout_id, bool trackChanges);
    }
}
