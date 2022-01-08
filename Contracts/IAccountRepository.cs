﻿using Entities.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IAccountRepository
    {
        AccountReturnAfterLogin getAccountByEmail(string email, bool trackChanges);
    }
}
