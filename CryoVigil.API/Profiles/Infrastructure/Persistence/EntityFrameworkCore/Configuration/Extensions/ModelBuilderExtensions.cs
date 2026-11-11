using CryoVigil.API.Profiles.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProfileConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserProfile>(entity =>
        {
            entity.ToTable("profiles");
            entity.HasKey(profile => profile.Id).HasName("pk_profiles");
            entity.Property(profile => profile.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
            entity.Property(profile => profile.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(profile => profile.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(254);
            entity.Property(profile => profile.Role)
                .HasColumnName("role")
                .IsRequired()
                .HasMaxLength(100);
        });
    }
}
