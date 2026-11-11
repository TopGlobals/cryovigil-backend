using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using CryoVigil.API.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using CryoVigil.API.Laboratories.Infrastructure.Persistence.Efc.Configuration.Extensions;
using CryoVigil.API.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using CryoVigil.API.Settings.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.UseSnakeCaseNamingConvention();
        builder.ApplyAlertsConfiguration();
        builder.ApplyLaboratoryConfiguration();
        builder.ApplyProfileConfiguration();
        builder.ApplySettingsConfiguration();
    }
}