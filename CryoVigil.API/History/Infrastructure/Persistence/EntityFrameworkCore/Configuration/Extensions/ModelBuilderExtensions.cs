using System.Text.Json;
using CryoVigil.API.History.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CryoVigil.API.History.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyHistoryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<HistoryEvent>(entity =>
        {
            entity.ToTable("history_events");
            entity.HasKey(e => e.Id).HasName("pk_history_events");
            entity.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
            entity.Property(e => e.TitleKey).HasColumnName("title_key").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Type).HasColumnName("type").IsRequired().HasMaxLength(50);
            entity.Property(e => e.MessageKey).HasColumnName("message_key").IsRequired().HasMaxLength(500);
            entity.Property(e => e.Severity).HasColumnName("severity").IsRequired().HasMaxLength(30);
            entity.Property(e => e.Location).HasColumnName("location").IsRequired().HasMaxLength(100);
            entity.Property(e => e.Sensor).HasColumnName("sensor").IsRequired().HasMaxLength(50);
            entity.Property(e => e.IsResolved).HasColumnName("is_resolved").IsRequired();
            var actionsTakenKeys = entity.Property(e => e.ActionsTakenKeys)
                .HasColumnName("actions_taken_keys")
                .HasConversion(
                    v => JsonSerializer.Serialize(v ?? new List<string>(), (JsonSerializerOptions?)null),
                    v => string.IsNullOrWhiteSpace(v)
                        ? new List<string>()
                        : JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>())
                .HasMaxLength(1000)
                .IsRequired(false);
            actionsTakenKeys.Metadata.SetValueComparer(new ValueComparer<List<string>>(
                    (left, right) => left != null && right != null && left.SequenceEqual(right),
                    value => value.Aggregate(0, (hash, item) => HashCode.Combine(hash, item.GetHashCode())),
                    value => value.ToList()));
            entity.Property(e => e.Timestamp).HasColumnName("timestamp").IsRequired();
        });
    }
}
