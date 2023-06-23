    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Medias;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Medias;

public class CommunityMediaOwnerConfiguration : IEntityTypeConfiguration<CommunityMediaOwner>
{
    public void Configure(EntityTypeBuilder<CommunityMediaOwner> builder)
    {
        builder.ToTable("community_media_owners");

        builder.HasKey(e => e.Id).HasName("PRIMARY");

        //builder.HasIndex(e => e.CommunityId, "FK_community_mediaowner_community_idx");
        //builder.HasIndex(e => e.MediaId, "FK_community_mediaowner_media_idx");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(e => e.CommunityId)
            .HasColumnName("user_id")
            .IsRequired();
        builder.Property(e => e.MediaId)
            .HasColumnName("media_id")
            .IsRequired();

        builder.HasOne(d => d.Community)
    .WithMany(p => p.Medias)
    .HasForeignKey(d => d.CommunityId)
    .OnDelete(DeleteBehavior.Cascade)
    .HasConstraintName("FK_community_mediaowner_community");

        builder.HasOne(d => d.Media)
            .WithOne(p => p.CommunityMediaOwner)
            .HasForeignKey<CommunityMediaOwner>(d => d.MediaId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_community_mediaowner_media");
    }
}
