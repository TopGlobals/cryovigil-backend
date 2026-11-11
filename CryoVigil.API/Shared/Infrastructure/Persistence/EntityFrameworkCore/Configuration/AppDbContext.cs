using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using CryoVigil.API.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using CryoVigil.API.History.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using CryoVigil.API.Laboratories.Infrastructure.Persistence.Efc.Configuration.Extensions;
using CryoVigil.API.Settings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        builder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyAlertsConfiguration();
        builder.ApplyLaboratoryConfiguration();
        builder.ApplySettingsConfiguration();
        builder.ApplyHistoryConfiguration();
        builder.UseSnakeCaseNamingConvention();
    }
}
