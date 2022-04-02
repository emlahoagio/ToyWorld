using Contracts;
using Contracts.Repositories;
using Contracts.Services;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Repository.Repository;
using Repository.Services;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IHasingServices _hasing;
        private IJwtSupport _jwtSupport;
        private IToyRepository _toyRepository;
        private IBrandRepository _brandRepository;
        private ITypeRepository _typeRepository;
        private IAccountRepository _accountRepository;
        private IGroupRepository _groupRepository;
        private IContestRepository _contestRepository;
        private IImageRepository _imageRepository;
        private IPostRepository _postRepository;
        private ITradingPostRepository _tradingPostRepository;
        private IReactPostRepository _reactPostRepository;
        private ICommentRepository _commentRepository;
        private IReactCommentRepository _reactCommentRepository;
        private IFollowAccountRepository _followAccountRepository;
        private IProposalRepository _proposalRepository;
        private IPrizeContestRepository _prizeContestRepository;
        private IPrizeRepository _prizeRepository;
        private IProposalPrizeRepository _proposalPrizeRepository;
        private IErrorLogsRepository _errorLogsRepository;
        private IJoinedContestRepository _joinedContestRepository;
        private IPostOfContestRepository _postOfContestRepository;
        private IRewardRepository _rewardRepository;
        private IRateRepository _rateRepository;
        private IBillRepository _billRepository;
        private INotificationRepository _notificationRepository;
        private IChatRepository _chatRepository;
        private IRateSellerRepository _rateSellerRepository;
        private IEvaluateContestRepository _evaluateContestRepository;
        private IFeedbackRepository _feedbackRepository;
        private IReactTradingPostRepository _reactTradingRepository;
        private IFavoriteTypeRepository _favoriteTypeRepository;
        private IFavoriteBrandRepository _favoriteBrandRepository;

        private IFollowGroupRepository _followGroupRepository;

        public RepositoryManager(DataContext context)
        {
            _context = context;
        }

        public RepositoryManager(DataContext context, IConfiguration configuration, IHasingServices hasing)
        {
            _context = context;
            _configuration = configuration;
            _hasing = hasing;
        }

        public IFollowGroupRepository FollowGroup
        {
            get
            {
                if (_followGroupRepository == null)
                {
                    _followGroupRepository = new FollowGroupRepository(_context);
                }
                return _followGroupRepository;
            }
        }

        public IAccountRepository Account
        {
            get
            {
                if (_accountRepository == null)
                {
                    if (_jwtSupport == null)
                    {
                        _jwtSupport = new JwtServices(_configuration);
                    }
                    _accountRepository = new AccountRepository(_context, _jwtSupport, _hasing);
                }
                return _accountRepository;
            }
        }

        public IToyRepository Toy
        {
            get
            {
                if (_toyRepository == null)
                {
                    _toyRepository = new ToyRepository(_context, _configuration, this);
                }
                return _toyRepository;
            }
        }

        public ITypeRepository Type
        {
            get
            {
                if (_typeRepository == null)
                {
                    _typeRepository = new TypeRepository(_context);
                }
                return _typeRepository;
            }
        }
        public IBrandRepository Brand
        {
            get
            {
                if (_brandRepository == null)
                {
                    _brandRepository = new BrandRepository(_context);
                }
                return _brandRepository;
            }
        }
        public IGroupRepository Group
        {
            get
            {
                if (_groupRepository == null)
                {
                    _groupRepository = new GroupRepository(_context);
                }
                return _groupRepository;
            }
        }

        public IContestRepository Contest
        {
            get
            {
                if (_contestRepository == null)
                {
                    _contestRepository = new ContestRepository(_context);
                }
                return _contestRepository;
            }
        }

        public IImageRepository Image
        {
            get
            {
                if (_imageRepository == null)
                {
                    _imageRepository = new ImageRepository(_context);
                }
                return _imageRepository;
            }
        }

        public IPostRepository Post
        {
            get
            {
                if (_postRepository == null)
                {
                    _postRepository = new PostRepository(_context);
                }
                return _postRepository;
            }
        }

        public IReactPostRepository ReactPost
        {
            get
            {
                if (_reactPostRepository == null)
                {
                    _reactPostRepository = new ReactPostRepository(_context);
                }
                return _reactPostRepository;
            }
        }

        public ITradingPostRepository TradingPost
        {
            get
            {
                if (_tradingPostRepository == null)
                {
                    _tradingPostRepository = new TradingPostRepository(_context);
                }
                return _tradingPostRepository;
            }
        }

        public ICommentRepository Comment
        {
            get
            {
                if (_commentRepository == null)
                {
                    _commentRepository = new CommentRepository(_context);
                }
                return _commentRepository;
            }
        }

        public IReactCommentRepository ReactComment
        {
            get
            {
                if (_reactCommentRepository == null)
                {
                    _reactCommentRepository = new ReactCommentRepository(_context);
                }
                return _reactCommentRepository;
            }
        }

        public IFollowAccountRepository FollowAccount
        {
            get
            {
                if (_followAccountRepository == null)
                {
                    _followAccountRepository = new FollowAccountRepository(_context);
                }
                return _followAccountRepository;
            }
        }

        public IProposalRepository Proposal
        {
            get
            {
                if (_proposalRepository == null)
                {
                    _proposalRepository = new ProposalRepository(_context);
                }
                return _proposalRepository;
            }
        }

        public IPrizeContestRepository PrizeContest
        {
            get
            {
                if (_prizeContestRepository == null)
                {
                    _prizeContestRepository = new PrizeContestRepository(_context);
                }
                return _prizeContestRepository;
            }
        }

        public IPrizeRepository Prize
        {
            get
            {
                if (_prizeRepository == null)
                {
                    _prizeRepository = new PrizeRepository(_context);
                }
                return _prizeRepository;
            }
        }

        public IProposalPrizeRepository ProposalPrize
        {
            get
            {
                if (_proposalPrizeRepository == null)
                    _proposalPrizeRepository = new ProposalPrizeRepository(_context);
                return _proposalPrizeRepository;
            }
        }

        public IErrorLogsRepository ErrorLog
        {
            get
            {
                if (_errorLogsRepository == null)
                {
                    _errorLogsRepository = new ErrorLogsRepository(_context);
                }
                return _errorLogsRepository;
            }
        }

        public IJoinedContestRepository JoinContest
        {
            get
            {
                if (_joinedContestRepository == null)
                {
                    _joinedContestRepository = new JoinedContestRepository(_context);
                }
                return _joinedContestRepository;
            }
        }

        public IPostOfContestRepository PostOfContest
        {
            get
            {
                if (_postOfContestRepository == null)
                    _postOfContestRepository = new PostOfContestRepository(_context);
                return _postOfContestRepository;
            }
        }

        public IRewardRepository Reward
        {
            get
            {
                if (_rewardRepository == null)
                    _rewardRepository = new RewardRepository(_context);
                return _rewardRepository;
            }
        }

        public IRateRepository Rate
        {
            get
            {
                if (_rateRepository == null)
                    _rateRepository = new RateRepository(_context);
                return _rateRepository;
            }
        }

        public INotificationRepository Notification
        {
            get
            {
                if (_notificationRepository == null)
                    _notificationRepository = new NotificationRepository(_context);
                return _notificationRepository;
            }
        }

        public IChatRepository Chat
        {
            get
            {
                if (_chatRepository == null)
                    _chatRepository = new ChatRepository(_context);
                return _chatRepository;
            }
        }

        public IBillRepository Bill
        {
            get
            {
                if (_billRepository == null)
                    _billRepository = new BillRepository(_context);
                return _billRepository;
            }
        }

        public IRateSellerRepository RateSeller
        {
            get
            {
                if (_rateSellerRepository == null)
                    _rateSellerRepository = new RateSellerRepository(_context);
                return _rateSellerRepository;
            }
        }

        public IEvaluateContestRepository EvaluateContest
        {
            get
            {
                if (_evaluateContestRepository == null)
                    _evaluateContestRepository = new EvaluateContestRepository(_context);
                return _evaluateContestRepository;
            }
        }

        public IFeedbackRepository Feedback
        {
            get
            {
                if (_feedbackRepository == null)
                    _feedbackRepository = new FeedbackRepository(_context);
                return _feedbackRepository;
            }
        }

        public IReactTradingPostRepository ReactTradingPost
        {
            get
            {
                if (_reactTradingRepository == null)
                    _reactTradingRepository = new ReactTradingPostRepository(_context);
                return _reactTradingRepository;
            }
        }

        public IFavoriteTypeRepository FavoriteType
        {
            get
            {
                if (_favoriteTypeRepository == null)
                    _favoriteTypeRepository = new FavoriteTypeRepository(_context);
                return _favoriteTypeRepository;
            }
        }

        public IFavoriteBrandRepository FavoriteBrand
        {
            get
            {
                if (_favoriteBrandRepository == null)
                    _favoriteBrandRepository = new FavoriteBrandRepository(_context);
                return _favoriteBrandRepository;
            }
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
