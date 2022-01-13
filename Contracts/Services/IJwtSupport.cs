using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IJwtSupport
    {
        string CreateToken(int role, int accountId);
    }
}
