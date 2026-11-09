using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Laboratories.Infrastructure.Persistence.Efc.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyLaboratoryConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Laboratory>(entity =>
        {
            entity.ToTable("laboratories");
            entity.HasKey(laboratory => laboratory.Id).HasName("pk_laboratories");
            entity.Property(laboratory => laboratory.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(laboratory => laboratory.Name).HasColumnName("name").IsRequired().HasMaxLength(100);
            entity.Property(laboratory => laboratory.Code).HasColumnName("code").IsRequired().HasMaxLength(50);
            entity.HasIndex(laboratory => laboratory.Code)
                .HasDatabaseName("ix_laboratories_code")
                .IsUnique();
            entity.Property(laboratory => laboratory.Building).HasColumnName("building").IsRequired().HasMaxLength(50);
            entity.Property(laboratory => laboratory.Floor).HasColumnName("floor").IsRequired().HasMaxLength(20);
            entity.Property(laboratory => laboratory.Room).HasColumnName("room").IsRequired().HasMaxLength(50);
            entity.Property(laboratory => laboratory.Type).HasColumnName("type").IsRequired().HasConversion<string>().HasMaxLength(30);
            entity.Property(laboratory => laboratory.Description).HasColumnName("description").IsRequired().HasMaxLength(500);
            entity.Property(laboratory => laboratory.TemperatureMin).HasColumnName("temperature_min").HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.TemperatureMax).HasColumnName("temperature_max").HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.Co2Max).HasColumnName("co2_max").IsRequired();
            entity.Property(laboratory => laboratory.GasSensitivity).HasColumnName("gas_sensitivity").IsRequired().HasConversion<string>().HasMaxLength(30);
            entity.Property(laboratory => laboratory.VibrationMax).HasColumnName("vibration_max").HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.Escalation).HasColumnName("escalation").IsRequired().HasConversion<string>().HasMaxLength(30);
            entity.Property(laboratory => laboratory.Status).HasColumnName("status").IsRequired().HasConversion<string>().HasMaxLength(20);
            entity.Property(laboratory => laboratory.Temperature).HasColumnName("temperature").HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.AirQuality).HasColumnName("air_quality");
            entity.Property(laboratory => laboratory.Unknown).HasColumnName("unknown");
            entity.Property(laboratory => laboratory.UpdatedAt).HasColumnName("updated_at");

            entity.OwnsOne(laboratory => laboratory.Sensors, sensors =>
            {
                sensors.Property(sensor => sensor.Temperature).HasColumnName("sensor_temperature").IsRequired();
                sensors.Property(sensor => sensor.AirQuality).HasColumnName("sensor_air_quality").IsRequired();
                sensors.Property(sensor => sensor.AiDetection).HasColumnName("sensor_ai_detection").IsRequired();
                sensors.Property(sensor => sensor.AirConditioning).HasColumnName("sensor_air_conditioning").IsRequired();
            });

            entity.OwnsOne(laboratory => laboratory.Notifications, notifications =>
            {
                notifications.Property(notification => notification.Email).HasColumnName("notification_email").IsRequired();
                notifications.Property(notification => notification.Sms).HasColumnName("notification_sms").IsRequired();
                notifications.Property(notification => notification.Push).HasColumnName("notification_push").IsRequired();
                notifications.Property(notification => notification.CriticalOnly).HasColumnName("notification_critical_only").IsRequired();
            });
        });
    }
}
