using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using CryoVigil.API.Laboratories.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Alerts.Infrastructure.Persistence.Efc.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAlertConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>(entity =>
        {
            entity.ToTable("alerts");
            entity.HasKey(alert => alert.Id).HasName("pk_alerts");
            entity.Property(alert => alert.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(alert => alert.Code).HasColumnName("code").IsRequired().HasMaxLength(50);
            entity.Property(alert => alert.Title).HasColumnName("title").IsRequired().HasMaxLength(100);
            entity.Property(alert => alert.Description).HasColumnName("description").IsRequired().HasMaxLength(500);
            entity.Property(alert => alert.Severity).HasColumnName("severity")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);
            entity.Property(alert => alert.Status).HasColumnName("status")
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(30);
            entity.Property(alert => alert.LaboratoryId).HasColumnName("laboratory_id").IsRequired();
            entity.Property(alert => alert.SensorId).HasColumnName("sensor_id").IsRequired();
            entity.Property(alert => alert.Reading).HasColumnName("reading").IsRequired();
            entity.Property(alert => alert.SafeMin).HasColumnName("safe_min").IsRequired();
            entity.Property(alert => alert.SafeMax).HasColumnName("safe_max").IsRequired();
            entity.Property(alert => alert.RaisedAt).HasColumnName("raised_at").IsRequired();

            entity.HasIndex(alert => alert.LaboratoryId)
                .HasDatabaseName("ix_alerts_laboratory_id");
            entity.HasOne<Laboratory>()
                .WithMany()
                .HasForeignKey(alert => alert.LaboratoryId)
                .HasConstraintName("fk_alerts_laboratories_laboratory_id")
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}