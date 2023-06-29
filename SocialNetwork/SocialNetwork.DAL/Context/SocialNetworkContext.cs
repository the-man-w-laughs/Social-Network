using MySqlModelBuilder = MySql.EntityFrameworkCore.Extensions.MySQLModelBuilderExtensions;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.Context;

public class SocialNetworkContext : DbContext
{
    public DbSet<Chat> Chats { get; set; } = null!;
    public DbSet<ChatMember> ChatMembers { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<CommentLike> CommentLikes { get; set; } = null!;    
    public DbSet<Community> Communities { get; set; } = null!;
    public DbSet<CommunityMember> CommunityMembers { get; set; } = null!;   
    public DbSet<Media> Medias { get; set; } = null!;
    public DbSet<Message> Messages { get; set; } = null!;
    public DbSet<MessageLike> MessageLikes { get; set; } = null!;
    public DbSet<MessageMedia> MessageMedias { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<PostLike> PostLikes { get; set; } = null!;    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserFollower> UserFollowers { get; set; } = null!;
    public DbSet<UserFriend> UserFriends { get; set; } = null!;
    public DbSet<UserProfile> UserProfiles { get; set; } = null!;        
    public DbSet<MediaLike> MediaLikes { get; set; } = null!;


    public SocialNetworkContext(DbContextOptions options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();        
    }   
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("utf8mb3_general_ci");
        MySqlModelBuilder.HasCharSet(modelBuilder, "utf8mb3");

        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());        
    }
}