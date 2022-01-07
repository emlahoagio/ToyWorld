using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IToyRepository
    {
        IEnumerable<ToyInList> GetAllToys(bool trackChanges);
    }
}
