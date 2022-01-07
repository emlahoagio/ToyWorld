using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IToyRepository
    {
        IEnumerable<Toy> GetAllToys(bool trackChanges);
    }

    public interface IBrandRepository
    {
    }

    public interface ITypeRepository
    {
    }
}
