using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IFavoriteTypeRepository
    {
        Task<List<Entities.Models.Type>> GetFavoriteType(int account_id, bool trackChanges);
        bool IsFavoriteType(List<Entities.Models.Type> types, int type_id);
        void Create(FavoriteType favorite);
        void Delete(FavoriteType favorite);
    }
}
