using CryoVigil.API.Alerts.Domain.Model.Aggregates;
using CryoVigil.API.Alerts.Domain.Model.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Alerts.Infrastructure.Persistence.Efc.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAlertConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alert>().HasKey(a => a.Id);
        modelBuilder.Entity<Alert>().Property(a => a.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Alert>().Property(a => a.Code).IsRequired().HasMaxLength(50);
        modelBuilder.Entity<Alert>().Property(a => a.Title).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Alert>().Property(a => a.Description).IsRequired().HasMaxLength(500);
        modelBuilder.Entity<Alert>().Property(a => a.Severity).IsRequired().HasConversion(v => v.ToString(), v => (AlertSeverity)Enum.Parse(typeof(AlertSeverity), v));
        modelBuilder.Entity<Alert>().Property(a => a.Status).IsRequired().HasConversion(v => v.ToString(), v => (AlertStatus)Enum.Parse(typeof(AlertStatus), v));
        modelBuilder.Entity<Alert>().Property(a => a.LaboratoryId).IsRequired();
        modelBuilder.Entity<Alert>().Property(a => a.SensorId).IsRequired();
        modelBuilder.Entity<Alert>().Property(a => a.Reading).IsRequired();
        modelBuilder.Entity<Alert>().Property(a => a.SafeMin).IsRequired();
        modelBuilder.Entity<Alert>().Property(a => a.SafeMax).IsRequired();
        modelBuilder.Entity<Alert>().Property(a => a.RaisedAt).IsRequired();
    }
}