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

namespace SocialNetwork.DAL.Context;

public partial class SocialNetworkContext : DbContext
{
    
    public virtual DbSet<Chat> Chats { get; set; } = null!;

    public virtual DbSet<ChatMember> ChatMembers { get; set; } = null!;

    public virtual DbSet<Comment> Comments { get; set; } = null!;

    public virtual DbSet<CommentLike> CommentLikes { get; set; } = null!;

    public virtual DbSet<CommentMedia> CommentMedias { get; set; } = null!;

    public virtual DbSet<Community> Communities { get; set; } = null!;

    public virtual DbSet<CommunityMember> CommunityMembers { get; set; } = null!;

    public virtual DbSet<CommunityPost> CommunityPosts { get; set; } = null!;

    public virtual DbSet<Media> Medias { get; set; } = null!;

    public virtual DbSet<Message> Messages { get; set; } = null!;

    public virtual DbSet<MessageLike> MessageLikes { get; set; } = null!;

    public virtual DbSet<MessageMedia> MessageMedias { get; set; } = null!;

    public virtual DbSet<Post> Posts { get; set; } = null!;

    public virtual DbSet<PostLike> PostLikes { get; set; } = null!;

    public virtual DbSet<PostMedia> PostMedias { get; set; } = null!;

    public virtual DbSet<User> Users { get; set; } = null!;

    public virtual DbSet<UserFollower> UserFollowers { get; set; } = null!;

    public virtual DbSet<UserFriend> UserFriends { get; set; } = null!;

    public virtual DbSet<UserProfile> UserProfiles { get; set; } = null!;

    public virtual DbSet<UserProfileMedia> UserProfileMedias { get; set; } = null!;

    public virtual DbSet<UserProfilePost> UserProfilePosts { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //TODO вроде этл должно конфигурироваться в api прям в высшем уровне но все же сделать здесь как нибудь нормально
            optionsBuilder.UseMySql("server=localhost;database=social_network;user=root;password=root",
               new MySqlServerVersion("8.0.32"));
        }
    }
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.ApplyConfiguration(new ChatConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMemberConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new CommentLikeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentMediaConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityMemberConfiguration());
        modelBuilder.ApplyConfiguration(new CommunityPostConfiguration());
        modelBuilder.ApplyConfiguration(new MediaConfiguration());
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
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
