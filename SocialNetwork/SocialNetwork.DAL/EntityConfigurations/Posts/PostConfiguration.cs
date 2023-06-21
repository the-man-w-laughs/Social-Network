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
    }
}
