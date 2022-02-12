using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IPrizeRepository
    {
        void CreatePrize(Prize prize);
        void UpdatePrize(Prize prize);
    }
}
