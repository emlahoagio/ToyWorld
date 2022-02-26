using Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IToyRepository Toy { get; }
        ITypeRepository Type { get; }
        IBrandRepository Brand { get; }
        IAccountRepository Account { get; }
        IPostRepository Post { get; }
        ITradingPostRepository TradingPost { get; }
        ICommentRepository Comment { get; }
        IGroupRepository Group { get; }
        IContestRepository Contest { get; }
        IImageRepository Image { get; }
        IReactPostRepository ReactPost { get; }
        IReactCommentRepository ReactComment { get; }
        IFollowAccountRepository FollowAccount { get; }
        IProposalRepository Proposal { get; }
        IPrizeContestRepository PrizeContest {get;}
        IPrizeRepository Prize { get; }
        IProposalPrizeRepository ProposalPrize { get; }
        IErrorLogsRepository ErrorLog { get; }
        Task SaveAsync();
    }
}
