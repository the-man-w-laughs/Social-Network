using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;
using SocialNetwork.DAL.EntityConfigurations.Chats;
using SocialNetwork.DAL.EntityConfigurations.Comments;
using SocialNetwork.DAL.EntityConfigurations.Communities;
using SocialNetwork.DAL.EntityConfigurations.Medias;
using SocialNetwork.DAL.EntityConfigurations.Messages;
using SocialNetwork.DAL.EntityConfigurations.Posts;
using SocialNetwork.DAL.EntityConfigurations.Users;
using ChatMembersType = SocialNetwork.DAL.Entities.Chats.ChatMembersType;
using CommentLike = SocialNetwork.DAL.Entities.Comments.CommentLike;
using CommentMedia = SocialNetwork.DAL.Entities.Comments.CommentMedia;
using CommunityMember = SocialNetwork.DAL.Entities.Communities.CommunityMember;
using CommunityMemberType = SocialNetwork.DAL.Entities.Communities.CommunityMemberType;
using CommunityPost = SocialNetwork.DAL.Entities.Communities.CommunityPost;
using MediaType = SocialNetwork.DAL.Entities.Medias.MediaType;
using MessageLike = SocialNetwork.DAL.Entities.Messages.MessageLike;
using MessageMedia = SocialNetwork.DAL.Entities.Messages.MessageMedia;
using PostLike = SocialNetwork.DAL.Entities.Posts.PostLike;
using PostMedia = SocialNetwork.DAL.Entities.Posts.PostMedia;
using User = SocialNetwork.DAL.Entities.Users.User;
using UserFollower = SocialNetwork.DAL.Entities.Users.UserFollower;
using UserFriend = SocialNetwork.DAL.Entities.Users.UserFriend;
using UserProfile = SocialNetwork.DAL.Entities.Users.UserProfile;
using UserProfileMedia = SocialNetwork.DAL.Entities.Users.UserProfileMedia;
using UserProfilePost = SocialNetwork.DAL.Entities.Users.UserProfilePost;
using UserType = SocialNetwork.DAL.Entities.Users.UserType;

namespace SocialNetwork.DAL.Context;

public partial class SocialNetworkContext : DbContext
{
    public SocialNetworkContext()
    {
    }

    public SocialNetworkContext(DbContextOptions<SocialNetworkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatMember> ChatMembers { get; set; }

    public virtual DbSet<ChatMembersType> ChatMembersTypes { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CommentLike> CommentLikes { get; set; }

    public virtual DbSet<CommentMedia> CommentMedias { get; set; }

    public virtual DbSet<Community> Communities { get; set; }

    public virtual DbSet<CommunityMember> CommunityMembers { get; set; }

    public virtual DbSet<CommunityMemberType> CommunityMemberTypes { get; set; }

    public virtual DbSet<CommunityPost> CommunityPosts { get; set; }

    public virtual DbSet<FriendshipType> FriendshipTypes { get; set; }

    public virtual DbSet<Media> Medias { get; set; }

    public virtual DbSet<MediaType> MediaTypes { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageLike> MessageLikes { get; set; }

    public virtual DbSet<MessageMedia> MessageMedias { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<PostLike> PostLikes { get; set; }

    public virtual DbSet<PostMedia> PostMedias { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserFollower> UserFollowers { get; set; }

    public virtual DbSet<UserFriend> UserFriends { get; set; }

    public virtual DbSet<UserProfile> UserProfiles { get; set; }

    public virtual DbSet<UserProfileMedia> UserProfileMedias { get; set; }

    public virtual DbSet<UserProfilePost> UserProfilePosts { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=social_network;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.ApplyConfiguration(new ChatConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMemberConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMembersTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CommentLikeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentMediaConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityMemberConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityMemberTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityPostConfiguration());
        modelBuilder.ApplyConfiguration(new FriendshipTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MediaConfiguration());
        modelBuilder.ApplyConfiguration(new MediaTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new MessageLikeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageMediaConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new PostLikeConfiguration());
        modelBuilder.ApplyConfiguration(new PostMediaConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new UserFollowerConfiguration());
        modelBuilder.ApplyConfiguration(new UserFriendConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileMediaConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfilePostConfiguration());
        modelBuilder.ApplyConfiguration(new UserTypeConfiguration());

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
