﻿using Entities.DataTransferObject;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IReactPostRepository
    {
        void CreateReact(ReactPost reactPost);
        void DeleteReact(ReactPost reactPost);
        Task<List<AccountReact>> GetAccountReactPost(int post_id, bool trackChanges);
    }
}
