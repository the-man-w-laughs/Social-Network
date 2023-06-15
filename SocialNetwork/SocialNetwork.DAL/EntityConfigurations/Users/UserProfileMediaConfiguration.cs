using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialNetwork.DAL.Entities.Users;

namespace SocialNetwork.DAL.EntityConfigurations.Users;

public  class UserProfileMediaConfiguration : IEntityTypeConfiguration<UserProfileMedia>
{
    public void Configure(EntityTypeBuilder<UserProfileMedia> builder)
    {
        builder.ToTable("user_profile_medias");

        builder.HasKey(e => e.Id).HasName("PRIMARY");
        
        builder.HasIndex(e => e.MediaId, "FK_user_profile_medias_medias_idx");
        builder.HasIndex(e => e.UserProfileId, "FK_user_profile_medias_user_profiles_idx");

        builder.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd()
            .IsRequired();
        builder.Property(e => e.MediaId)
            .HasColumnName("media_id")
            .IsRequired();
        builder.Property(e => e.UserProfileId)
            .HasColumnName("user_profile_id")
            .IsRequired();
        
        builder.HasOne(d => d.Media).WithMany(p => p.UserProfileMedia)
            .HasForeignKey(d => d.MediaId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_profile_medias_medias");

        builder.HasOne(d => d.UserProfile).WithMany(p => p.UserProfileMedia)
            .HasForeignKey(d => d.UserProfileId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_user_profile_medias_user_profiles");
    }
}
