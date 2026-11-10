using CryoVigil.API.Settings.Domain.Model.Aggregates;
using CryoVigil.API.Settings.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Settings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySettingsConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeneralSettings>(entity =>
        {
            entity.ToTable("settings");
            entity.HasKey(settings => settings.Id).HasName("pk_general_settings");
            entity.Property(settings => settings.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
            entity.Property(settings => settings.Key)
                .HasColumnName("setting_key")
                .IsRequired()
                .HasMaxLength(30);
            entity.HasIndex(settings => settings.Key)
                .HasDatabaseName("ix_settings_key")
                .IsUnique();
            entity.Property(settings => settings.Region)
                .HasColumnName("region")
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(settings => settings.TemperatureUnit)
                .HasColumnName("temperature_unit")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.OwnsOne(settings => settings.Profile, profile =>
            {
                profile.Property(value => value.Name).HasColumnName("profile_name").IsRequired().HasMaxLength(100);
                profile.Property(value => value.Email).HasColumnName("profile_email").IsRequired().HasMaxLength(254);
                profile.Property(value => value.Role).HasColumnName("profile_role").IsRequired().HasMaxLength(100);
            });

            entity.OwnsOne(settings => settings.Notifications, notifications =>
            {
                notifications.Property(value => value.Email).HasColumnName("notification_email").IsRequired();
                notifications.Property(value => value.InApp).HasColumnName("notification_in_app").IsRequired();
                notifications.Property(value => value.DailySummary).HasColumnName("notification_daily_summary").IsRequired();
                notifications.Property(value => value.WeeklyReport).HasColumnName("notification_weekly_report").IsRequired();
                notifications.Property(value => value.InstantAlerts).HasColumnName("notification_instant_alerts").IsRequired();
                notifications.Property(value => value.WarningThreshold).HasColumnName("warning_threshold").IsRequired().HasMaxLength(10);
                notifications.Property(value => value.CriticalThreshold).HasColumnName("critical_threshold").IsRequired().HasMaxLength(10);
            });

            entity.OwnsOne(settings => settings.Security, security =>
            {
                security.Property(value => value.TwoFactorEnabled).HasColumnName("two_factor_enabled").IsRequired();
            });

            entity.OwnsMany(settings => settings.Sensors, sensors =>
            {
                sensors.ToTable("settings_sensors");
                sensors.WithOwner().HasForeignKey("SettingsId");
                sensors.Property<int>("SettingsId").HasColumnName("settings_id");
                sensors.Property(sensor => sensor.Id)
                    .HasColumnName("id")
                    .HasColumnType("char(36)")
                    .ValueGeneratedNever();
                sensors.HasKey("SettingsId", nameof(MonitoringSensor.Id))
                    .HasName("pk_settings_sensors");
                sensors.Property(sensor => sensor.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
                sensors.Property(sensor => sensor.Model).HasColumnName("model").IsRequired().HasMaxLength(100);
                sensors.Property(sensor => sensor.Serial).HasColumnName("serial").IsRequired().HasMaxLength(100);
                sensors.Property(sensor => sensor.Type).HasColumnName("type").IsRequired().HasMaxLength(50);
                sensors.Property(sensor => sensor.Location).HasColumnName("location").IsRequired().HasMaxLength(100);
                sensors.Property(sensor => sensor.Status).HasColumnName("status").IsRequired().HasMaxLength(20);
            });
        });
    }
}
