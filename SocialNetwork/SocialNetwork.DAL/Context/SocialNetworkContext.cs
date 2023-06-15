using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Entities.Chats;
using SocialNetwork.DAL.Entities.Comments;
using SocialNetwork.DAL.Entities.Communities;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Messages;
using SocialNetwork.DAL.Entities.Posts;
using SocialNetwork.DAL.Entities.Users;

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

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.ChatId).HasName("PRIMARY");

            entity.ToTable("chats");

            entity.HasIndex(e => e.ChatId, "chat_id_UNIQUE").IsUnique();

            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.CreatedAt)
                .HasMaxLength(45)
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<ChatMember>(entity =>
        {
            entity.HasKey(e => e.UserChatId).HasName("PRIMARY");

            entity.ToTable("chat_members");

            entity.HasIndex(e => e.ChatMemberType, "FK_chat_members_chat_members_types_idx");

            entity.HasIndex(e => e.ChatId, "FK_chat_members_chats_idx");

            entity.HasIndex(e => e.UserId, "FK_chat_members_users_idx");

            entity.HasIndex(e => e.UserChatId, "user_chat_id_UNIQUE").IsUnique();

            entity.Property(e => e.UserChatId).HasColumnName("user_chat_id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.ChatMemberType).HasColumnName("chat_member_type");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatMembers)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chat_members_chats");

            entity.HasOne(d => d.ChatMemberTypeNavigation).WithMany(p => p.ChatMembers)
                .HasForeignKey(d => d.ChatMemberType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chat_members_chat_members_types");

            entity.HasOne(d => d.User).WithMany(p => p.ChatMembers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_chat_members_users");
        });

        modelBuilder.Entity<ChatMembersType>(entity =>
        {
            entity.HasKey(e => e.ChatMembersTypeId).HasName("PRIMARY");

            entity.ToTable("chat_members_types");

            entity.Property(e => e.ChatMembersTypeId).HasColumnName("chat_members_type_id");
            entity.Property(e => e.ChatMembersTypeName)
                .HasMaxLength(45)
                .HasColumnName("chat_members_type_name");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PRIMARY");

            entity.ToTable("comments");

            entity.HasIndex(e => e.RepliedComment, "FK_comments_comments_idx");

            entity.HasIndex(e => e.PostId, "FK_comments_posts_idx");

            entity.HasIndex(e => e.AuthorId, "FK_comments_users_idx");

            entity.Property(e => e.CommentId)
                .ValueGeneratedNever()
                .HasColumnName("comment_id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.RepliedComment).HasColumnName("replied_comment");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Author).WithMany(p => p.Comments)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comments_users");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comments_posts");

            entity.HasOne(d => d.RepliedCommentNavigation).WithMany(p => p.InverseRepliedCommentNavigation)
                .HasForeignKey(d => d.RepliedComment)
                .HasConstraintName("FK_comments_comments");
        });

        modelBuilder.Entity<CommentLike>(entity =>
        {
            entity.HasKey(e => e.CommentLikeId).HasName("PRIMARY");

            entity.ToTable("comment_likes");

            entity.HasIndex(e => e.CommentId, "FK_comment_likes_comments_idx");

            entity.HasIndex(e => e.UserId, "FK_comment_likes_users_idx");

            entity.Property(e => e.CommentLikeId)
                .ValueGeneratedNever()
                .HasColumnName("comment_like_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comment_likes_comments");

            entity.HasOne(d => d.User).WithMany(p => p.CommentLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comment_likes_users");
        });

        modelBuilder.Entity<CommentMedia>(entity =>
        {
            entity.HasKey(e => e.CommentMediaId).HasName("PRIMARY");

            entity.ToTable("comment_medias");

            entity.HasIndex(e => e.CommentId, "FK_comment_medias_comments_idx");

            entity.HasIndex(e => e.MediaId, "FK_comment_medias_medias_idx");

            entity.Property(e => e.CommentMediaId)
                .ValueGeneratedNever()
                .HasColumnName("comment_media_id");
            entity.Property(e => e.CommentId).HasColumnName("comment_id");
            entity.Property(e => e.MediaId).HasColumnName("media_id");

            entity.HasOne(d => d.Comment).WithMany(p => p.CommentMedia)
                .HasForeignKey(d => d.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comment_medias_comments");

            entity.HasOne(d => d.Media).WithMany(p => p.CommentMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_comment_medias_medias");
        });

        modelBuilder.Entity<Community>(entity =>
        {
            entity.HasKey(e => e.CommunityId).HasName("PRIMARY");

            entity.ToTable("communities");

            entity.Property(e => e.CommunityId)
                .ValueGeneratedNever()
                .HasColumnName("community_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsPrivate).HasColumnName("is_private");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<CommunityMember>(entity =>
        {
            entity.HasKey(e => e.CommunityMemberId).HasName("PRIMARY");

            entity.ToTable("community_members");

            entity.HasIndex(e => e.CommunityId, "FK_community_members_communities_idx");

            entity.HasIndex(e => e.CommunityMemberTypeId, "FK_community_members_community_member_types_idx");

            entity.HasIndex(e => e.UserId, "FK_community_members_users_idx");

            entity.Property(e => e.CommunityMemberId)
                .ValueGeneratedNever()
                .HasColumnName("community_member_id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.CommunityMemberTypeId).HasColumnName("community_member_type_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Community).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK_community_members_communities");

            entity.HasOne(d => d.CommunityMemberType).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.CommunityMemberTypeId)
                .HasConstraintName("FK_community_members_community_member_types");

            entity.HasOne(d => d.User).WithMany(p => p.CommunityMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_community_members_users");
        });

        modelBuilder.Entity<CommunityMemberType>(entity =>
        {
            entity.HasKey(e => e.CommunityMemberTypeId).HasName("PRIMARY");

            entity.ToTable("community_member_types");

            entity.Property(e => e.CommunityMemberTypeId).HasColumnName("community_member_type_id");
            entity.Property(e => e.TypeName)
                .HasMaxLength(45)
                .HasColumnName("type_name");
        });

        modelBuilder.Entity<CommunityPost>(entity =>
        {
            entity.HasKey(e => e.CommunityPostId).HasName("PRIMARY");

            entity.ToTable("community_posts");

            entity.HasIndex(e => e.CommunityId, "FK_community_posts_communities_idx");

            entity.HasIndex(e => e.PostId, "FK_community_posts_posts_idx");

            entity.HasIndex(e => e.ProposerId, "FK_community_posts_users_idx");

            entity.Property(e => e.CommunityPostId)
                .ValueGeneratedNever()
                .HasColumnName("community_post_id");
            entity.Property(e => e.CommunityId).HasColumnName("community_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.ProposerId).HasColumnName("proposer_id");

            entity.HasOne(d => d.Community).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.CommunityId)
                .HasConstraintName("FK_community_posts_communities");

            entity.HasOne(d => d.Post).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_community_posts_posts");

            entity.HasOne(d => d.Proposer).WithMany(p => p.CommunityPosts)
                .HasForeignKey(d => d.ProposerId)
                .HasConstraintName("FK_community_posts_users");
        });

        modelBuilder.Entity<FriendshipType>(entity =>
        {
            entity.HasKey(e => e.FriendshipTypeId).HasName("PRIMARY");

            entity.ToTable("friendship_types");

            entity.Property(e => e.FriendshipTypeId).HasColumnName("friendship_type_id");
            entity.Property(e => e.FriendshipType1)
                .HasMaxLength(45)
                .HasColumnName("friendship_type");
        });

        modelBuilder.Entity<Media>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PRIMARY");

            entity.ToTable("medias");

            entity.HasIndex(e => e.MediaType, "FK_medias_media_types_idx");

            entity.Property(e => e.MediaId)
                .ValueGeneratedNever()
                .HasColumnName("media_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityType)
                .HasMaxLength(45)
                .HasColumnName("entity_type");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(1024)
                .HasColumnName("file_path");
            entity.Property(e => e.MediaType).HasColumnName("media_type");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.MediaTypeNavigation).WithMany(p => p.Media)
                .HasForeignKey(d => d.MediaType)
                .HasConstraintName("FK_medias_media_types");
        });

        modelBuilder.Entity<MediaType>(entity =>
        {
            entity.HasKey(e => e.MediaTypeId).HasName("PRIMARY");

            entity.ToTable("media_types");

            entity.Property(e => e.MediaTypeId).HasColumnName("media_type_id");
            entity.Property(e => e.MediaType1)
                .HasMaxLength(45)
                .HasColumnName("media_type");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PRIMARY");

            entity.ToTable("messages");

            entity.HasIndex(e => e.SenderId, "FK_messages_chat_members_idx");

            entity.HasIndex(e => e.ChatId, "FK_messages_chats_idx");

            entity.HasIndex(e => e.RepliedMessageId, "FK_messages_messages_idx");

            entity.HasIndex(e => e.MessageId, "message_id_UNIQUE").IsUnique();

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.RepliedMessageId).HasColumnName("replied_message_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(45)
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Chat).WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_messages_chats");

            entity.HasOne(d => d.RepliedMessage).WithMany(p => p.InverseRepliedMessage)
                .HasForeignKey(d => d.RepliedMessageId)
                .HasConstraintName("FK_messages_messages");
        });

        modelBuilder.Entity<MessageLike>(entity =>
        {
            entity.HasKey(e => e.MessageLikeId).HasName("PRIMARY");

            entity.ToTable("message_likes");

            entity.HasIndex(e => e.ChatMemberId, "FK_message_likes_chat_members_idx");

            entity.HasIndex(e => e.MessageId, "FK_message_likes_messages_idx");

            entity.Property(e => e.MessageLikeId)
                .ValueGeneratedNever()
                .HasColumnName("message_like_id");
            entity.Property(e => e.ChatMemberId).HasColumnName("chat_member_id");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.ChatMember).WithMany(p => p.MessageLikes)
                .HasForeignKey(d => d.ChatMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_likes_chat_members");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageLikes)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_likes_messages");
        });

        modelBuilder.Entity<MessageMedia>(entity =>
        {
            entity.HasKey(e => e.MessageMediaId).HasName("PRIMARY");

            entity.ToTable("message_medias");

            entity.HasIndex(e => e.ChatId, "FK_message_medias_chats_idx");

            entity.HasIndex(e => e.MediaId, "FK_message_medias_medias_idx");

            entity.HasIndex(e => e.MessageId, "FK_message_medias_messages_idx");

            entity.Property(e => e.MessageMediaId)
                .ValueGeneratedNever()
                .HasColumnName("message_media_id");
            entity.Property(e => e.ChatId).HasColumnName("chat_id");
            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Chat).WithMany(p => p.MessageMedia)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_medias_chats");

            entity.HasOne(d => d.Media).WithMany(p => p.MessageMedia)
                .HasForeignKey(d => d.MediaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_medias_medias");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageMedia)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_message_medias_messages");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PRIMARY");

            entity.ToTable("posts");

            entity.HasIndex(e => e.RepostId, "FK_posts_posts_idx");

            entity.Property(e => e.PostId)
                .ValueGeneratedNever()
                .HasColumnName("post_id");
            entity.Property(e => e.Content)
                .HasMaxLength(45)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.RepostId).HasColumnName("repost_id");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(45)
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Repost).WithMany(p => p.InverseRepost)
                .HasForeignKey(d => d.RepostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_posts_posts");
        });

        modelBuilder.Entity<PostLike>(entity =>
        {
            entity.HasKey(e => e.PostLikeId).HasName("PRIMARY");

            entity.ToTable("post_likes");

            entity.HasIndex(e => e.PostId, "FK_post_likes_posts_idx");

            entity.HasIndex(e => e.UserId, "FK_post_likes_users_idx");

            entity.Property(e => e.PostLikeId)
                .ValueGeneratedNever()
                .HasColumnName("post_like_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_post_likes_posts");

            entity.HasOne(d => d.User).WithMany(p => p.PostLikes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_post_likes_users");
        });

        modelBuilder.Entity<PostMedia>(entity =>
        {
            entity.HasKey(e => e.PostMediaId).HasName("PRIMARY");

            entity.ToTable("post_medias");

            entity.HasIndex(e => e.MediaId, "FK_post_medias_medias_idx");

            entity.HasIndex(e => e.PostId, "FK_post_medias_posts_idx");

            entity.Property(e => e.PostMediaId)
                .ValueGeneratedNever()
                .HasColumnName("post_media_id");
            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");

            entity.HasOne(d => d.Media).WithMany(p => p.PostMedia)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_post_medias_medias");

            entity.HasOne(d => d.Post).WithMany(p => p.PostMedia)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_post_medias_posts");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.UserTypeId, "FK_users_user_types_idx");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeactivatedAt)
                .HasColumnType("datetime")
                .HasColumnName("deactivated_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("datetime")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IsDeactivated).HasColumnName("is_deactivated");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.LastActiveAt)
                .HasColumnType("datetime")
                .HasColumnName("last_active_at");
            entity.Property(e => e.Login)
                .HasMaxLength(20)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(32)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Salt)
                .HasMaxLength(20)
                .HasColumnName("salt");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("FK_users_user_types");
        });

        modelBuilder.Entity<UserFollower>(entity =>
        {
            entity.HasKey(e => e.UserFollowerId).HasName("PRIMARY");

            entity.ToTable("user_followers");

            entity.HasIndex(e => e.SourceId, "FK_user_followers_source_users_idx");

            entity.HasIndex(e => e.TargetId, "FK_user_followers_target_users_idx");

            entity.Property(e => e.UserFollowerId)
                .ValueGeneratedNever()
                .HasColumnName("user_follower_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.SourceId).HasColumnName("source_id");
            entity.Property(e => e.TargetId).HasColumnName("target_id");

            entity.HasOne(d => d.Source).WithMany(p => p.UserFollowerSources)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("FK_user_followers_source_users");

            entity.HasOne(d => d.Target).WithMany(p => p.UserFollowerTargets)
                .HasForeignKey(d => d.TargetId)
                .HasConstraintName("FK_user_followers_target_users");
        });

        modelBuilder.Entity<UserFriend>(entity =>
        {
            entity.HasKey(e => e.UserFriendId).HasName("PRIMARY");

            entity.ToTable("user_friends");

            entity.HasIndex(e => e.FriendshipType, "FK_user_friends_friendship_types_idx");

            entity.HasIndex(e => e.User1Id, "FK_user_friends_users_1_idx");

            entity.HasIndex(e => e.User2Id, "FK_user_friends_users_2_idx");

            entity.Property(e => e.UserFriendId)
                .ValueGeneratedNever()
                .HasColumnName("user_friend_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FriendshipType).HasColumnName("friendship_type");
            entity.Property(e => e.User1Id).HasColumnName("user_1_id");
            entity.Property(e => e.User2Id).HasColumnName("user_2_id");

            entity.HasOne(d => d.FriendshipTypeNavigation).WithMany(p => p.UserFriends)
                .HasForeignKey(d => d.FriendshipType)
                .HasConstraintName("FK_user_friends_friendship_types");

            entity.HasOne(d => d.User1).WithMany(p => p.UserFriendUser1s)
                .HasForeignKey(d => d.User1Id)
                .HasConstraintName("FK_user_friends_users_1");

            entity.HasOne(d => d.User2).WithMany(p => p.UserFriendUser2s)
                .HasForeignKey(d => d.User2Id)
                .HasConstraintName("FK_user_friends_users_2");
        });

        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.HasKey(e => e.UserProfileId).HasName("PRIMARY");

            entity.ToTable("user_profiles");

            entity.HasIndex(e => e.UserId, "FK_user_profiles_users_idx");

            entity.Property(e => e.UserProfileId)
                .ValueGeneratedNever()
                .HasColumnName("user_profile_id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserCountry)
                .HasMaxLength(45)
                .HasColumnName("user_country");
            entity.Property(e => e.UserEducation)
                .HasMaxLength(45)
                .HasColumnName("user_education");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .HasColumnName("user_email");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(45)
                .HasColumnName("user_name");
            entity.Property(e => e.UserSex)
                .HasMaxLength(45)
                .HasColumnName("user_sex");
            entity.Property(e => e.UserSurname)
                .HasMaxLength(45)
                .HasColumnName("user_surname");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfiles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_user_profiles_users");
        });

        modelBuilder.Entity<UserProfileMedia>(entity =>
        {
            entity.HasKey(e => e.UserProfileMediaId).HasName("PRIMARY");

            entity.ToTable("user_profile_medias");

            entity.HasIndex(e => e.MediaId, "FK_user_profile_medias_medias_idx");

            entity.HasIndex(e => e.UserProfileId, "FK_user_profile_medias_user_profiles_idx");

            entity.Property(e => e.UserProfileMediaId)
                .ValueGeneratedNever()
                .HasColumnName("user_profile_media_id");
            entity.Property(e => e.MediaId).HasColumnName("media_id");
            entity.Property(e => e.UserProfileId).HasColumnName("user_profile_id");

            entity.HasOne(d => d.Media).WithMany(p => p.UserProfileMedia)
                .HasForeignKey(d => d.MediaId)
                .HasConstraintName("FK_user_profile_medias_medias");

            entity.HasOne(d => d.UserProfile).WithMany(p => p.UserProfileMedia)
                .HasForeignKey(d => d.UserProfileId)
                .HasConstraintName("FK_user_profile_medias_user_profiles");
        });

        modelBuilder.Entity<UserProfilePost>(entity =>
        {
            entity.HasKey(e => e.UserPostId).HasName("PRIMARY");

            entity.ToTable("user_profile_posts");

            entity.HasIndex(e => e.PostId, "FK_user_profile_posts_posts_idx");

            entity.HasIndex(e => e.UserId, "FK_user_profile_posts_users_idx");

            entity.Property(e => e.UserPostId)
                .ValueGeneratedNever()
                .HasColumnName("user_post_id");
            entity.Property(e => e.PostId).HasColumnName("post_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Post).WithMany(p => p.UserProfilePosts)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_user_profile_posts_posts");

            entity.HasOne(d => d.User).WithMany(p => p.UserProfilePosts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_user_profile_posts_users");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PRIMARY");

            entity.ToTable("user_types");

            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");
            entity.Property(e => e.UserTypeName)
                .HasMaxLength(45)
                .HasColumnName("user_type_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
