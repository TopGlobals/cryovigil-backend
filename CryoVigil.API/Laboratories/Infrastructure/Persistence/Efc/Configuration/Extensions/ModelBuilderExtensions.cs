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
            entity.Property(laboratory => laboratory.Id).ValueGeneratedOnAdd();
            entity.Property(laboratory => laboratory.Name).IsRequired().HasMaxLength(100);
            entity.Property(laboratory => laboratory.Code).IsRequired().HasMaxLength(50);
            entity.HasIndex(laboratory => laboratory.Code).IsUnique();
            entity.Property(laboratory => laboratory.Building).IsRequired().HasMaxLength(50);
            entity.Property(laboratory => laboratory.Floor).IsRequired().HasMaxLength(20);
            entity.Property(laboratory => laboratory.Room).IsRequired().HasMaxLength(50);
            entity.Property(laboratory => laboratory.Type).IsRequired().HasConversion<string>().HasMaxLength(30);
            entity.Property(laboratory => laboratory.Description).IsRequired().HasMaxLength(500);
            entity.Property(laboratory => laboratory.TemperatureMin).HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.TemperatureMax).HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.Co2Max).IsRequired();
            entity.Property(laboratory => laboratory.GasSensitivity).IsRequired().HasConversion<string>().HasMaxLength(30);
            entity.Property(laboratory => laboratory.VibrationMax).HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.Escalation).IsRequired().HasConversion<string>().HasMaxLength(30);
            entity.Property(laboratory => laboratory.Status).IsRequired().HasConversion<string>().HasMaxLength(20);
            entity.Property(laboratory => laboratory.Temperature).HasPrecision(8, 2);
            entity.Property(laboratory => laboratory.AirQuality);
            entity.Property(laboratory => laboratory.Unknown);
            entity.Property(laboratory => laboratory.UpdatedAt);

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
