using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.Entities;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.ValueConverters;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAlertsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Alert>(entity =>
        {
            entity.ToTable("alerts");
            entity.HasKey(a => a.Id).HasName("pk_alerts");
            entity.Property(a => a.Id)
                .HasColumnName("id")
                .HasConversion(new StronglyTypedIdConverter<AlertId, int>(id => id.Value, val => new AlertId(val)))
                .ValueGeneratedOnAdd();

            entity.Property(a => a.Code).HasColumnName("code").IsRequired().HasMaxLength(50);
            entity.HasIndex(a => a.Code).HasDatabaseName("ix_alerts_code").IsUnique();

            entity.Property(a => a.Title).HasColumnName("title").IsRequired().HasMaxLength(80);
            entity.Property(a => a.Message).HasColumnName("message").IsRequired().HasMaxLength(500);

            entity.Property(a => a.Severity)
                .HasColumnName("severity")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(a => a.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(a => a.SourceType)
                .HasColumnName("source_type")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(a => a.LaboratoryName).HasColumnName("laboratory_name").IsRequired().HasMaxLength(100);
            entity.Property(a => a.StorageUnitName).HasColumnName("storage_unit_name").HasMaxLength(100);
            entity.Property(a => a.SensorCode).HasColumnName("sensor_code").HasMaxLength(50);

            entity.Property(a => a.ReadingCelsius).HasColumnName("reading_celsius");
            entity.Property(a => a.SafeMinCelsius).HasColumnName("safe_min_celsius");
            entity.Property(a => a.SafeMaxCelsius).HasColumnName("safe_max_celsius");

            entity.Property(a => a.RaisedAt).HasColumnName("raised_at").IsRequired();
            entity.Property(a => a.AcknowledgedAt).HasColumnName("acknowledged_at");
            entity.Property(a => a.AcknowledgedBy).HasColumnName("acknowledged_by").HasMaxLength(100);
            entity.Property(a => a.ResolvedAt).HasColumnName("resolved_at");
            entity.Property(a => a.ResolvedBy).HasColumnName("resolved_by").HasMaxLength(100);

            entity.HasMany(a => a.CorrectiveActions)
                .WithOne()
                .HasForeignKey(ca => ca.AlertId)
                .HasConstraintName("fk_corrective_actions_alerts_alert_id")
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Readings)
                .WithOne()
                .HasForeignKey(r => r.AlertId)
                .HasConstraintName("fk_alert_readings_alerts_alert_id")
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CorrectiveAction>(entity =>
        {
            entity.ToTable("corrective_actions");
            entity.HasKey(ca => ca.Id).HasName("pk_corrective_actions");
            entity.Property(ca => ca.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(ca => ca.AlertId)
                .HasColumnName("alert_id")
                .HasConversion(new StronglyTypedIdConverter<AlertId, int>(id => id.Value, val => new AlertId(val)))
                .IsRequired();
            entity.HasIndex(ca => ca.AlertId).HasDatabaseName("ix_corrective_actions_alert_id");
            entity.Property(ca => ca.Description).HasColumnName("description").IsRequired().HasMaxLength(500);
            entity.Property(ca => ca.TechnicianName).HasColumnName("technician_name").IsRequired().HasMaxLength(100);
            entity.Property(ca => ca.TakenAt).HasColumnName("taken_at").IsRequired();
        });

        builder.Entity<AlertReading>(entity =>
        {
            entity.ToTable("alert_readings");
            entity.HasKey(r => r.Id).HasName("pk_alert_readings");
            entity.Property(r => r.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(r => r.AlertId)
                .HasColumnName("alert_id")
                .HasConversion(new StronglyTypedIdConverter<AlertId, int>(id => id.Value, val => new AlertId(val)))
                .IsRequired();
            entity.HasIndex(r => r.AlertId).HasDatabaseName("ix_alert_readings_alert_id");
            entity.Property(r => r.TakenAt).HasColumnName("taken_at").IsRequired();
            entity.Property(r => r.ValueCelsius).HasColumnName("value_celsius").IsRequired();
        });
    }
}
