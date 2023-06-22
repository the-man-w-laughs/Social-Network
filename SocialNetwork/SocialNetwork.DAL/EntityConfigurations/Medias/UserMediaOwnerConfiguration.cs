using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Medias;

public class UserMediaOwnerConfiguration : IEntityTypeConfiguration<UserMediaOwner>
{
    public void Configure(EntityTypeBuilder<UserMediaOwner> builder)
    {
        builder.ToTable("user_media_owners");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.UserId, "FK_user_mediaowner_user_idx");
        builder.HasIndex(e => e.MediaId, "FK_user_mediaowner_media_idx");

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

        builder.HasOne(d => d.User)
    .WithMany(p => p.Medias)
    .HasForeignKey(d => d.UserId)
    .OnDelete(DeleteBehavior.Cascade)
    .HasConstraintName("FK_user_mediaowner_user");

        builder.HasOne(d => d.Media)
            .WithOne(p => p.UserMediaOwner)
            .HasForeignKey<UserMediaOwner>(d => d.MediaId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_mediaowner_media");
    }
}
