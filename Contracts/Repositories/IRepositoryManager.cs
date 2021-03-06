using Contracts.Repositories;
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
        IPrizeContestRepository PrizeContest { get; }
        IPrizeRepository Prize { get; }
        IErrorLogsRepository ErrorLog { get; }
        IJoinedContestRepository JoinContest { get; }
        IPostOfContestRepository PostOfContest { get; }
        IRewardRepository Reward { get; }
        IRateRepository Rate { get; }
        INotificationRepository Notification { get; } //quandtm add
        IChatRepository Chat { get; } //quandtm add
        IBillRepository Bill { get; }
        IRateSellerRepository RateSeller { get; }
        IEvaluateContestRepository EvaluateContest { get; }
        IFeedbackRepository Feedback { get; }
        IReactTradingPostRepository ReactTradingPost { get; }
        IFavoriteTypeRepository FavoriteType { get; }
        IFavoriteBrandRepository FavoriteBrand { get; }
        IFollowGroupRepository FollowGroup { get; }

        IProposalRepository Proposal { get; }

        Task SaveAsync();
    }
}
