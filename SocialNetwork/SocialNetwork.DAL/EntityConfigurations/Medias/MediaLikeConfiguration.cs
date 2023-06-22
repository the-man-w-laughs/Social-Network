using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public class MediaLikeConfiguration : IEntityTypeConfiguration<MediaLike>
{
    public void Configure(EntityTypeBuilder<MediaLike> builder)
    {
        builder.ToTable("media_likes");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.UserId, "FK_media_likes_media_idx");
        builder.HasIndex(e => e.MediaId, "FK_media_likes_users_idx");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.UserId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(e => e.MediaId)
            .HasColumnName("media_id")
            .IsRequired();

        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired()
            .HasColumnType("datetime").HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(d => d.Media).WithMany(p => p.MediaLikes)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_media_likes_media");

        builder.HasOne(d => d.User).WithMany(p => p.MediaLikes)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_media_likes_users");
    }
}
