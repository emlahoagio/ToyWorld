using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Entities.Models
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Contest> Contests { get; set; }
        public virtual DbSet<Evaluate> Evaluates { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FollowAccount> FollowAccounts { get; set; }
        public virtual DbSet<FollowGroup> FollowGroups { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ManageGroup> ManageGroups { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Prize> Prizes { get; set; }
        public virtual DbSet<PrizeContest> PrizeContests { get; set; }
        public virtual DbSet<ReactComment> ReactComments { get; set; }
        public virtual DbSet<ReactPost> ReactPosts { get; set; }
        public virtual DbSet<ReactTradingPost> ReactTradingPosts { get; set; }
        public virtual DbSet<Toy> Toys { get; set; }
        public virtual DbSet<TradingPost> TradingPosts { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<PostOfContest> PostOfContests { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<ErrorLogs> Error { get; set; }
        public virtual DbSet<JoinedToContest> JoinedToContest { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Chat> Chat { get; set; }
        public virtual DbSet<Bill> Bill { get; set; }
        public virtual DbSet<RateSeller> RateSeller { get; set; }
        public virtual DbSet<FavoriteType> FavoriteType { get; set; }

        public virtual DbSet<Proposal> Proposal { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tws.cdbycqrte9ll.us-east-2.rds.amazonaws.com;Database=TWS;user id=admin;password=capstone2022;Trusted_Connection=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Proposal>(entity =>
                {
                    entity.HasOne(d => d.Account)
                    .WithMany(p => p.Proposals)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Proposal_Account_AccountId");
                }
            );

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar).IsUnicode(false);

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Gender).IsUnicode(false);

                entity.Property(e => e.Uid).IsUnicode(false);

            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Comment_Account");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Comment_Post");
            });

            modelBuilder.Entity<Contest>(entity =>
            {
                entity.ToTable("Contest");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.EndRegistration).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.StartRegistration).HasColumnType("datetime");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Contests)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Contest_Brand");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Contests)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Contest_Type");
            });

            modelBuilder.Entity<Evaluate>(entity =>
            {
                entity.HasKey(e => new { e.ContestId, e.AccountId });

                entity.ToTable("Evaluate");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluate_Account");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.Evaluates)
                    .HasForeignKey(d => d.ContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Evaluate_Contest");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("Feedback");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FeedbackAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Feedback_Account");

                entity.HasOne(d => d.AccountReply)
                    .WithMany(p => p.FeedbackAccountReplies)
                    .HasForeignKey(d => d.AccountReplyId)
                    .HasConstraintName("FK_Feedback_Account2");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Feedback_Post");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.FeedbackSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK_Feedback_Account1");

                entity.HasOne(d => d.TradingPost)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.TradingPostId)
                    .HasConstraintName("FK_Feedback_TradingPost");

                entity.HasOne(d => d.PostOfCotest)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.PostOfContestId)
                    .HasConstraintName("FK_Feedback_PostOfContest");
            });

            modelBuilder.Entity<FollowAccount>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.AccountFollowId });

                entity.ToTable("FollowAccount");

                entity.HasOne(d => d.AccountFollow)
                    .WithMany(p => p.FollowAccountAccountFollows)
                    .HasForeignKey(d => d.AccountFollowId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowAccount_Account1");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FollowAccountAccounts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowAccount_Account");
            });

            modelBuilder.Entity<FavoriteType>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.TypeId });

                entity.ToTable("FavoriteType");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FavoriteTypes)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Account_FavoriteTypeAccount");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FavoriteTypes)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Account_FavoriteTypeType");
            });

            modelBuilder.Entity<FavoriteBrand>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.BrandId });

                entity.ToTable("FavoriteBrand");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FavoriteBrands)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Brand_Account_FavoriteBrandAccount");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.FavoriteBrands)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Account_FavoriteBrandBrand");
            });

            modelBuilder.Entity<FollowGroup>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.GroupId });

                entity.ToTable("FollowGroup");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FollowGroups)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowGroup_Account");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.FollowGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FollowGroup_Group");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Url).IsUnicode(false);

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Image_Post");

                entity.HasOne(d => d.Prize)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.PrizeId)
                    .HasConstraintName("FK_Image_Prize");

                entity.HasOne(d => d.Toy)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.ToyId)
                    .HasConstraintName("FK_Image_Toy");

                entity.HasOne(d => d.TradingPost)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.TradingPostId)
                    .HasConstraintName("FK_Image_TradingPost");

                entity.HasOne(d => d.Bill)
                    .WithMany(p => p.Images)
                    .HasForeignKey(d => d.BillId)
                    .HasConstraintName("FK_Image_Bill");
            });

            modelBuilder.Entity<ManageGroup>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.GroupId });

                entity.ToTable("ManageGroup");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ManageGroups)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManageGroup_Account");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.ManageGroups)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ManageGroup_Group");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Post_Account");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_Post_Group");

                entity.HasOne(d => d.Toy)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ToyId)
                    .HasConstraintName("FK_Post_Toy");
            });

            modelBuilder.Entity<Prize>(entity =>
            {
                entity.ToTable("Prize");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<PrizeContest>(entity =>
            {
                entity.HasKey(e => new { e.ContestId, e.PrizeId });

                entity.ToTable("Prize_Contest");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.PrizeContests)
                    .HasForeignKey(d => d.ContestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prize_Contest_Contest");

                entity.HasOne(d => d.Prize)
                    .WithMany(p => p.PrizeContests)
                    .HasForeignKey(d => d.PrizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Prize_Contest_Prize");
            });

            modelBuilder.Entity<ReactComment>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.CommentId });

                entity.ToTable("ReactComment");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ReactComments)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReactComm__Accou__339FAB6E");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.ReactComments)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReactComm__Comme__3493CFA7");
            });

            modelBuilder.Entity<ReactPost>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.PostId });

                entity.ToTable("ReactPost");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ReactPosts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReactPost_Account");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.ReactPosts)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReactPost_Post");
            });

            modelBuilder.Entity<ReactTradingPost>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.TradingPostId });

                entity.ToTable("ReactTradingPost");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ReactTradingPosts)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReactTradingPost_Account");

                entity.HasOne(d => d.TradingPost)
                    .WithMany(p => p.ReactTradingPosts)
                    .HasForeignKey(d => d.TradingPostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReactTradingPost_TradingPost");
            });

            modelBuilder.Entity<Toy>(entity =>
            {
                entity.ToTable("Toy");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Price).HasColumnType("decimal(11, 2)");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Toys)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_Toy_Brand");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Toys)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Toy_Type");
            });

            modelBuilder.Entity<TradingPost>(entity =>
            {
                entity.ToTable("TradingPost");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Value).HasColumnType("decimal(9, 2)");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TradingPosts)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_TradingPost_Account");

                entity.HasOne(d => d.Toy)
                    .WithMany(p => p.TradingPosts)
                    .HasForeignKey(d => d.ToyId)
                    .HasConstraintName("FK_TradingPost_Toy");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TradingPosts)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK_TradingPost_Group");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<PostOfContest>(entity =>
            {
                entity.ToTable("PostOfContest");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK_PostOfContest_Contest");
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.ToTable("Rate");

                entity.HasOne(d => d.PostOfContest)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.PostOfContestId)
                    .HasConstraintName("FK_Rate_PostOfContest");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Rates)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_RateContest_Account");
            });

            modelBuilder.Entity<JoinedToContest>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.ContestId });

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ContestJoined)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Account_JoinedContest");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.AccountJoined)
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK_Contest_JoinedAccount");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Rewards)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Reward_Account");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.Rewards)
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK_Reward_Contest");

                entity.HasOne(d => d.Prize)
                    .WithMany(p => p.Rewards)
                    .HasForeignKey(d => d.PrizeId)
                    .HasConstraintName("FK_Reward_Prize");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Notification_Account");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Notification_Post");

                entity.HasOne(d => d.TradingPost)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.TradingPostId)
                    .HasConstraintName("FK_Notification_TradingPost");

                entity.HasOne(d => d.Contest)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.ContestId)
                    .HasConstraintName("FK_Notification_Contest");
            });

            //quandtm modify
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Chats)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Chat_Account_AccountId");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.BillsSeler)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK_Bill_Account_Seller");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.BillsBuyer)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK_Bill_Account_Buyer");

                entity.HasOne(d => d.TradingPost)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.TradingPostId)
                    .HasConstraintName("FK_Bill_TradingPost");
            });

            modelBuilder.Entity<RateSeller>(entity =>
            {
                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.RateSellersSeller)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK_RateSeller_Account_Seller");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.RateSellersBuyer)
                    .HasForeignKey(d => d.BuyerId)
                    .HasConstraintName("FK_RateSeller_Account_Buyer");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
