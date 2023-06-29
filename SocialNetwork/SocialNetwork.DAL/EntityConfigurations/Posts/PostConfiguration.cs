using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Posts;

namespace SocialNetwork.DAL.EntityConfigurations.Posts;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.RepostId, "FK_posts_posts_idx");

        builder.Property(e => e.Id).HasColumnName("id").IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(e => e.Content).HasColumnName("content")
            .HasColumnType("text")
            .HasMaxLength(Constants.PostContentMaxLength);
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").HasColumnType("datetime");

        builder.Property(e => e.RepostId).HasColumnName("repost_id");
        
        builder.HasOne(d => d.Repost).WithMany(p => p.InverseRepost)
            .HasForeignKey(d => d.RepostId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_posts_posts");

        builder.HasMany(c => c.Attachments)
            .WithMany(s => s.Posts)
            .UsingEntity(j =>
            {
                j.ToTable("posts_attachments");
            });

        builder.Property(e => e.CommunityId).HasColumnName("community_id")
        .IsRequired(false);

        builder.HasOne(d => d.Community).WithMany(p => p.CommunityPosts)
            .HasForeignKey(d => d.CommunityId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_community_posts_communities");

        builder.HasOne(d => d.Author).WithMany(p => p.Posts)
            .HasForeignKey(d => d.AuthorId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_users_posts_users");
    }
}
