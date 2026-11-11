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
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id)
                .HasConversion(new StronglyTypedIdConverter<AlertId, int>(id => id.Value, val => new AlertId(val)))
                .ValueGeneratedOnAdd();

            entity.Property(a => a.Code).IsRequired().HasMaxLength(50);
            entity.HasIndex(a => a.Code).IsUnique();

            entity.Property(a => a.Title).IsRequired().HasMaxLength(80);
            entity.Property(a => a.Message).IsRequired().HasMaxLength(500);

            entity.Property(a => a.Severity)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(a => a.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20);

            entity.Property(a => a.SourceType)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            entity.Property(a => a.LaboratoryName).IsRequired().HasMaxLength(100);
            entity.Property(a => a.StorageUnitName).HasMaxLength(100);
            entity.Property(a => a.SensorCode).HasMaxLength(50);

            entity.Property(a => a.ReadingCelsius);
            entity.Property(a => a.SafeMinCelsius);
            entity.Property(a => a.SafeMaxCelsius);

            entity.Property(a => a.RaisedAt).IsRequired();
            entity.Property(a => a.AcknowledgedAt);
            entity.Property(a => a.AcknowledgedBy).HasMaxLength(100);
            entity.Property(a => a.ResolvedAt);
            entity.Property(a => a.ResolvedBy).HasMaxLength(100);

            entity.HasMany(a => a.CorrectiveActions)
                .WithOne()
                .HasForeignKey(ca => ca.AlertId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(a => a.Readings)
                .WithOne()
                .HasForeignKey(r => r.AlertId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CorrectiveAction>(entity =>
        {
            entity.HasKey(ca => ca.Id);
            entity.Property(ca => ca.Id).ValueGeneratedOnAdd();
            entity.Property(ca => ca.AlertId)
                .HasConversion(new StronglyTypedIdConverter<AlertId, int>(id => id.Value, val => new AlertId(val)))
                .IsRequired();
            entity.Property(ca => ca.Description).IsRequired().HasMaxLength(500);
            entity.Property(ca => ca.TechnicianName).IsRequired().HasMaxLength(100);
            entity.Property(ca => ca.TakenAt).IsRequired();
        });

        builder.Entity<AlertReading>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).ValueGeneratedOnAdd();
            entity.Property(r => r.AlertId)
                .HasConversion(new StronglyTypedIdConverter<AlertId, int>(id => id.Value, val => new AlertId(val)))
                .IsRequired();
            entity.Property(r => r.TakenAt).IsRequired();
            entity.Property(r => r.ValueCelsius).IsRequired();
        });
    }
}
