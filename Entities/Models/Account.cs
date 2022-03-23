using System;
using System.Collections.Generic;

#nullable disable

namespace Entities.Models
{
    public partial class Account
    {
        public Account()
        {
            Comments = new HashSet<Comment>();
            Evaluates = new HashSet<Evaluate>();
            FeedbackAccountReplies = new HashSet<Feedback>();
            FeedbackAccounts = new HashSet<Feedback>();
            FeedbackSenders = new HashSet<Feedback>();
            FollowAccountAccountFollows = new HashSet<FollowAccount>();
            FollowAccountAccounts = new HashSet<FollowAccount>();
            FollowGroups = new HashSet<FollowGroup>();
            ManageGroups = new HashSet<ManageGroup>();
            Posts = new HashSet<Post>();
            Proposals = new HashSet<Proposal>();
            ReactComments = new HashSet<ReactComment>();
            ReactPosts = new HashSet<ReactPost>();
            ReactTradingPosts = new HashSet<ReactTradingPost>();
            TradingPosts = new HashSet<TradingPost>();
            ContestJoined = new HashSet<JoinedToContest>();
            PostOfContests = new HashSet<PostOfContest>();
            Rewards = new HashSet<Reward>();
            Rates = new HashSet<Rate>();
            Notifications = new HashSet<Notification>();
            Senders = new HashSet<Chat>(); //quandtm modify
            BillsBuyer = new HashSet<Bill>();
            BillsSeler = new HashSet<Bill>();
            RateSellersBuyer = new HashSet<RateSeller>();
            RateSellersSeller = new HashSet<RateSeller>();
        }

        public int Id { get; set; }
        public string Uid { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Biography { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
        public string Gender { get; set; }
        public bool Status { get; set; }
        public int? Role { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Evaluate> Evaluates { get; set; }
        public virtual ICollection<Feedback> FeedbackAccountReplies { get; set; }
        public virtual ICollection<Feedback> FeedbackAccounts { get; set; }
        public virtual ICollection<Feedback> FeedbackSenders { get; set; }
        public virtual ICollection<FollowAccount> FollowAccountAccountFollows { get; set; }
        public virtual ICollection<FollowAccount> FollowAccountAccounts { get; set; }
        public virtual ICollection<FollowGroup> FollowGroups { get; set; }
        public virtual ICollection<ManageGroup> ManageGroups { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Proposal> Proposals { get; set; }
        public virtual ICollection<ReactComment> ReactComments { get; set; }
        public virtual ICollection<ReactPost> ReactPosts { get; set; }
        public virtual ICollection<ReactTradingPost> ReactTradingPosts { get; set; }
        public virtual ICollection<TradingPost> TradingPosts { get; set; }
        public virtual ICollection<JoinedToContest> ContestJoined { get; set; }
        public virtual ICollection<PostOfContest> PostOfContests { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Chat> Senders { get; set; } //quandtm modify
        public virtual ICollection<Bill> BillsSeler { get; set; }
        public virtual ICollection<Bill> BillsBuyer { get; set; }
        public virtual ICollection<RateSeller> RateSellersBuyer { get; set; }
        public virtual ICollection<RateSeller> RateSellersSeller { get; set; }

    }
}
