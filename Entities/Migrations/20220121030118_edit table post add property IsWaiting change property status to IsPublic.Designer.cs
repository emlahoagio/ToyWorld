﻿// <auto-generated />
using System;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220121030118_edit table post add property IsWaiting change property status to IsPublic")]
    partial class edittablepostaddpropertyIsWaitingchangepropertystatustoIsPublic
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Email")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<int?>("Role")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Uid")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("Entities.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Brand");
                });

            modelBuilder.Entity("Entities.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("PostId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Entities.Models.Contest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<bool>("CanAttempt")
                        .HasColumnType("bit");

                    b.Property<string>("CoverImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndRegistration")
                        .HasColumnType("datetime");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("MaxRegistration")
                        .HasColumnType("int");

                    b.Property<int?>("MinRegistration")
                        .HasColumnType("int");

                    b.Property<int?>("ProposalId")
                        .HasColumnType("int");

                    b.Property<string>("Slogan")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("StartRegistration")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.Property<string>("Venue")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("ProposalId");

                    b.HasIndex("TypeId");

                    b.ToTable("Contest");
                });

            modelBuilder.Entity("Entities.Models.Evaluate", b =>
                {
                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NoOfStart")
                        .HasColumnType("int");

                    b.HasKey("ContestId", "AccountId");

                    b.HasIndex("AccountId");

                    b.ToTable("Evaluate");
                });

            modelBuilder.Entity("Entities.Models.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("AccountReplyId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<string>("ReplyContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int");

                    b.Property<int?>("TradingPostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("AccountReplyId");

                    b.HasIndex("PostId");

                    b.HasIndex("SenderId");

                    b.ToTable("Feedback");
                });

            modelBuilder.Entity("Entities.Models.FollowAccount", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("AccountFollowId")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "AccountFollowId");

                    b.HasIndex("AccountFollowId");

                    b.ToTable("FollowAccount");
                });

            modelBuilder.Entity("Entities.Models.FollowGroup", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("FollowGroup");
                });

            modelBuilder.Entity("Entities.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("Entities.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ContestId")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int?>("PrizeId")
                        .HasColumnType("int");

                    b.Property<int?>("ProposalId")
                        .HasColumnType("int");

                    b.Property<int?>("ToyId")
                        .HasColumnType("int");

                    b.Property<int?>("TradingPostId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContestId");

                    b.HasIndex("PostId");

                    b.HasIndex("PrizeId");

                    b.HasIndex("ProposalId");

                    b.HasIndex("ToyId");

                    b.HasIndex("TradingPostId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Entities.Models.ManageGroup", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("ManageGroup");
                });

            modelBuilder.Entity("Entities.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsPublic")
                        .HasColumnType("bit");

                    b.Property<bool?>("IsWaiting")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("PostDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("PublicDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ToyId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("GroupId");

                    b.HasIndex("ToyId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("Entities.Models.Prize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Prize");
                });

            modelBuilder.Entity("Entities.Models.PrizeContest", b =>
                {
                    b.Property<int>("ContestId")
                        .HasColumnType("int");

                    b.Property<int>("PrizeId")
                        .HasColumnType("int");

                    b.HasKey("ContestId", "PrizeId");

                    b.HasIndex("PrizeId");

                    b.ToTable("Prize_Contest");
                });

            modelBuilder.Entity("Entities.Models.Proposal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("ContestDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("MaxRegister")
                        .HasColumnType("int");

                    b.Property<int?>("MinRegister")
                        .HasColumnType("int");

                    b.Property<bool?>("Status")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("BrandId");

                    b.HasIndex("TypeId");

                    b.ToTable("Proposal");
                });

            modelBuilder.Entity("Entities.Models.ProposalPrize", b =>
                {
                    b.Property<int>("ProposalId")
                        .HasColumnType("int");

                    b.Property<int>("PrizeId")
                        .HasColumnType("int");

                    b.HasKey("ProposalId", "PrizeId");

                    b.HasIndex("PrizeId");

                    b.ToTable("Proposal_Prize");
                });

            modelBuilder.Entity("Entities.Models.ReactComment", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "CommentId");

                    b.HasIndex("CommentId");

                    b.ToTable("ReactComment");
                });

            modelBuilder.Entity("Entities.Models.ReactPost", b =>
                {
                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("AccountId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("ReactPost");
                });

            modelBuilder.Entity("Entities.Models.Toy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BrandId")
                        .HasColumnType("int");

                    b.Property<string>("CoverImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(11,2)");

                    b.Property<int?>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BrandId");

                    b.HasIndex("TypeId");

                    b.ToTable("Toy");
                });

            modelBuilder.Entity("Entities.Models.TradingPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ToyId")
                        .HasColumnType("int");

                    b.Property<string>("ToyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Trading")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Value")
                        .HasColumnType("decimal(9,2)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ToyId");

                    b.ToTable("TradingPost");
                });

            modelBuilder.Entity("Entities.Models.Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Type");
                });

            modelBuilder.Entity("Entities.Models.Comment", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("Comments")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Comment_Account");

                    b.HasOne("Entities.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_Comment_Post");

                    b.Navigation("Account");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Entities.Models.Contest", b =>
                {
                    b.HasOne("Entities.Models.Brand", "Brand")
                        .WithMany("Contests")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("FK_Contest_Brand");

                    b.HasOne("Entities.Models.Proposal", "Proposal")
                        .WithMany("Contests")
                        .HasForeignKey("ProposalId")
                        .HasConstraintName("FK_Contest_Proposal");

                    b.HasOne("Entities.Models.Type", "Type")
                        .WithMany("Contests")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK_Contest_Type");

                    b.Navigation("Brand");

                    b.Navigation("Proposal");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Entities.Models.Evaluate", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("Evaluates")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Evaluate_Account")
                        .IsRequired();

                    b.HasOne("Entities.Models.Contest", "Contest")
                        .WithMany("Evaluates")
                        .HasForeignKey("ContestId")
                        .HasConstraintName("FK_Evaluate_Contest")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Contest");
                });

            modelBuilder.Entity("Entities.Models.Feedback", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("FeedbackAccounts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Feedback_Account");

                    b.HasOne("Entities.Models.Account", "AccountReply")
                        .WithMany("FeedbackAccountReplies")
                        .HasForeignKey("AccountReplyId")
                        .HasConstraintName("FK_Feedback_Account2");

                    b.HasOne("Entities.Models.Post", "Post")
                        .WithMany("Feedbacks")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_Feedback_Post");

                    b.HasOne("Entities.Models.Account", "Sender")
                        .WithMany("FeedbackSenders")
                        .HasForeignKey("SenderId")
                        .HasConstraintName("FK_Feedback_Account1");

                    b.Navigation("Account");

                    b.Navigation("AccountReply");

                    b.Navigation("Post");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Entities.Models.FollowAccount", b =>
                {
                    b.HasOne("Entities.Models.Account", "AccountFollow")
                        .WithMany("FollowAccountAccountFollows")
                        .HasForeignKey("AccountFollowId")
                        .HasConstraintName("FK_FollowAccount_Account1")
                        .IsRequired();

                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("FollowAccountAccounts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_FollowAccount_Account")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("AccountFollow");
                });

            modelBuilder.Entity("Entities.Models.FollowGroup", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("FollowGroups")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_FollowGroup_Account")
                        .IsRequired();

                    b.HasOne("Entities.Models.Group", "Group")
                        .WithMany("FollowGroups")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_FollowGroup_Group")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Entities.Models.Image", b =>
                {
                    b.HasOne("Entities.Models.Contest", "Contest")
                        .WithMany("Images")
                        .HasForeignKey("ContestId")
                        .HasConstraintName("FK_Image_Contest");

                    b.HasOne("Entities.Models.Post", "Post")
                        .WithMany("Images")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_Image_Post");

                    b.HasOne("Entities.Models.Prize", "Prize")
                        .WithMany("Images")
                        .HasForeignKey("PrizeId")
                        .HasConstraintName("FK_Image_Prize");

                    b.HasOne("Entities.Models.Proposal", "Proposal")
                        .WithMany("Images")
                        .HasForeignKey("ProposalId")
                        .HasConstraintName("FK_Image_Proposal");

                    b.HasOne("Entities.Models.Toy", "Toy")
                        .WithMany("Images")
                        .HasForeignKey("ToyId")
                        .HasConstraintName("FK_Image_Toy");

                    b.HasOne("Entities.Models.TradingPost", "TradingPost")
                        .WithMany("Images")
                        .HasForeignKey("TradingPostId")
                        .HasConstraintName("FK_Image_TradingPost");

                    b.Navigation("Contest");

                    b.Navigation("Post");

                    b.Navigation("Prize");

                    b.Navigation("Proposal");

                    b.Navigation("Toy");

                    b.Navigation("TradingPost");
                });

            modelBuilder.Entity("Entities.Models.ManageGroup", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("ManageGroups")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_ManageGroup_Account")
                        .IsRequired();

                    b.HasOne("Entities.Models.Group", "Group")
                        .WithMany("ManageGroups")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_ManageGroup_Group")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("Entities.Models.Post", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("Posts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Post_Account");

                    b.HasOne("Entities.Models.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK_Post_Group");

                    b.HasOne("Entities.Models.Toy", "Toy")
                        .WithMany("Posts")
                        .HasForeignKey("ToyId")
                        .HasConstraintName("FK_Post_Toy");

                    b.Navigation("Account");

                    b.Navigation("Group");

                    b.Navigation("Toy");
                });

            modelBuilder.Entity("Entities.Models.PrizeContest", b =>
                {
                    b.HasOne("Entities.Models.Contest", "Contest")
                        .WithMany("PrizeContests")
                        .HasForeignKey("ContestId")
                        .HasConstraintName("FK_Prize_Contest_Contest")
                        .IsRequired();

                    b.HasOne("Entities.Models.Prize", "Prize")
                        .WithMany("PrizeContests")
                        .HasForeignKey("PrizeId")
                        .HasConstraintName("FK_Prize_Contest_Prize")
                        .IsRequired();

                    b.Navigation("Contest");

                    b.Navigation("Prize");
                });

            modelBuilder.Entity("Entities.Models.Proposal", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("Proposals")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_Proposal_Account");

                    b.HasOne("Entities.Models.Brand", "Brand")
                        .WithMany("Proposals")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("FK_Proposal_Brand");

                    b.HasOne("Entities.Models.Type", "Type")
                        .WithMany("Proposals")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK_Proposal_Type");

                    b.Navigation("Account");

                    b.Navigation("Brand");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Entities.Models.ProposalPrize", b =>
                {
                    b.HasOne("Entities.Models.Prize", "Prize")
                        .WithMany("ProposalPrizes")
                        .HasForeignKey("PrizeId")
                        .HasConstraintName("FK_Proposal_Prize_Prize")
                        .IsRequired();

                    b.HasOne("Entities.Models.Proposal", "Proposal")
                        .WithMany("ProposalPrizes")
                        .HasForeignKey("ProposalId")
                        .HasConstraintName("FK_Proposal_Prize_Proposal")
                        .IsRequired();

                    b.Navigation("Prize");

                    b.Navigation("Proposal");
                });

            modelBuilder.Entity("Entities.Models.ReactComment", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("ReactComments")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK__ReactComm__Accou__339FAB6E")
                        .IsRequired();

                    b.HasOne("Entities.Models.Comment", "Comment")
                        .WithMany("ReactComments")
                        .HasForeignKey("CommentId")
                        .HasConstraintName("FK__ReactComm__Comme__3493CFA7")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Comment");
                });

            modelBuilder.Entity("Entities.Models.ReactPost", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("ReactPosts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_ReactPost_Account")
                        .IsRequired();

                    b.HasOne("Entities.Models.Post", "Post")
                        .WithMany("ReactPosts")
                        .HasForeignKey("PostId")
                        .HasConstraintName("FK_ReactPost_Post")
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("Entities.Models.Toy", b =>
                {
                    b.HasOne("Entities.Models.Brand", "Brand")
                        .WithMany("Toys")
                        .HasForeignKey("BrandId")
                        .HasConstraintName("FK_Toy_Brand");

                    b.HasOne("Entities.Models.Type", "Type")
                        .WithMany("Toys")
                        .HasForeignKey("TypeId")
                        .HasConstraintName("FK_Toy_Type");

                    b.Navigation("Brand");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Entities.Models.TradingPost", b =>
                {
                    b.HasOne("Entities.Models.Account", "Account")
                        .WithMany("TradingPosts")
                        .HasForeignKey("AccountId")
                        .HasConstraintName("FK_TradingPost_Account");

                    b.HasOne("Entities.Models.Toy", "Toy")
                        .WithMany("TradingPosts")
                        .HasForeignKey("ToyId")
                        .HasConstraintName("FK_TradingPost_Toy");

                    b.Navigation("Account");

                    b.Navigation("Toy");
                });

            modelBuilder.Entity("Entities.Models.Account", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Evaluates");

                    b.Navigation("FeedbackAccountReplies");

                    b.Navigation("FeedbackAccounts");

                    b.Navigation("FeedbackSenders");

                    b.Navigation("FollowAccountAccountFollows");

                    b.Navigation("FollowAccountAccounts");

                    b.Navigation("FollowGroups");

                    b.Navigation("ManageGroups");

                    b.Navigation("Posts");

                    b.Navigation("Proposals");

                    b.Navigation("ReactComments");

                    b.Navigation("ReactPosts");

                    b.Navigation("TradingPosts");
                });

            modelBuilder.Entity("Entities.Models.Brand", b =>
                {
                    b.Navigation("Contests");

                    b.Navigation("Proposals");

                    b.Navigation("Toys");
                });

            modelBuilder.Entity("Entities.Models.Comment", b =>
                {
                    b.Navigation("ReactComments");
                });

            modelBuilder.Entity("Entities.Models.Contest", b =>
                {
                    b.Navigation("Evaluates");

                    b.Navigation("Images");

                    b.Navigation("PrizeContests");
                });

            modelBuilder.Entity("Entities.Models.Group", b =>
                {
                    b.Navigation("FollowGroups");

                    b.Navigation("ManageGroups");

                    b.Navigation("Posts");
                });

            modelBuilder.Entity("Entities.Models.Post", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Feedbacks");

                    b.Navigation("Images");

                    b.Navigation("ReactPosts");
                });

            modelBuilder.Entity("Entities.Models.Prize", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("PrizeContests");

                    b.Navigation("ProposalPrizes");
                });

            modelBuilder.Entity("Entities.Models.Proposal", b =>
                {
                    b.Navigation("Contests");

                    b.Navigation("Images");

                    b.Navigation("ProposalPrizes");
                });

            modelBuilder.Entity("Entities.Models.Toy", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Posts");

                    b.Navigation("TradingPosts");
                });

            modelBuilder.Entity("Entities.Models.TradingPost", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("Entities.Models.Type", b =>
                {
                    b.Navigation("Contests");

                    b.Navigation("Proposals");

                    b.Navigation("Toys");
                });
#pragma warning restore 612, 618
        }
    }
}
