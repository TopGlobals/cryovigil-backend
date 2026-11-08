using CryoVigil.API.Settings.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Settings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySettingsConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeneralSettings>(entity =>
        {
            entity.ToTable("general_settings");
            entity.HasKey(settings => settings.Id).HasName("pk_general_settings");
            entity.Property(settings => settings.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
            entity.Property(settings => settings.Region)
                .HasColumnName("region")
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(settings => settings.TemperatureUnit)
                .HasColumnName("temperature_unit")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);
        });
    }
}
