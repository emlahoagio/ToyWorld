﻿using Entities.DataTransferObject;
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
        Task<IEnumerable<ToyInList>> GetAllToys(ToyParameters toyParameters, bool trackChanges);
    }
}
