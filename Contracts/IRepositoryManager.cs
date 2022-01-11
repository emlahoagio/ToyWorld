using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IToyRepository Toy { get; }
        ITypeRepository Type { get; }
        IBrandRepository Brand { get; }
        IAccountRepository Account { get; }
        IGroupRepository Group { get; }
        IContestRepository Contest { get; }
        void Save();
    }
}
