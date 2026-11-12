using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using CryoVigil.API.Alerts.Application.CommandServices;
using CryoVigil.API.Alerts.Application.Internal.CommandServices;
using CryoVigil.API.Alerts.Application.Internal.QueryServices;
using CryoVigil.API.Alerts.Application.QueryServices;
using CryoVigil.API.Alerts.Domain.Repositories;
using CryoVigil.API.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using CryoVigil.API.OperationEvents.Application.CommandServices;
using CryoVigil.API.OperationEvents.Application.Internal.CommandServices;
using CryoVigil.API.OperationEvents.Application.Internal.QueryServices;
using CryoVigil.API.OperationEvents.Application.QueryServices;
using CryoVigil.API.OperationEvents.Domain.Repositories;
using CryoVigil.API.OperationEvents.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using CryoVigil.API.Settings.Application.CommandServices;
using CryoVigil.API.Settings.Application.Internal.CommandServices;
using CryoVigil.API.Settings.Application.Internal.QueryServices;
using CryoVigil.API.Settings.Application.QueryServices;
using CryoVigil.API.Settings.Domain.Repositories;
using CryoVigil.API.Settings.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using CryoVigil.API.Laboratories.Application.CommandServices;
using CryoVigil.API.Laboratories.Application.Internal.CommandServices;
using CryoVigil.API.Laboratories.Application.Internal.QueryServices;
using CryoVigil.API.Laboratories.Application.QueryServices;
using CryoVigil.API.Laboratories.Domain.Repositories;
using CryoVigil.API.Laboratories.Infrastructure.Persistence.Efc.Repositories;
using CryoVigil.API.Profiles.Application.CommandServices;
using CryoVigil.API.Profiles.Application.Internal.CommandServices;
using CryoVigil.API.Profiles.Application.Internal.QueryServices;
using CryoVigil.API.Profiles.Application.QueryServices;
using CryoVigil.API.Profiles.Domain.Repositories;
using CryoVigil.API.Profiles.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using CryoVigil.API.Shared.Domain.Repositories;
using CryoVigil.API.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using CryoVigil.API.Shared.Infrastructure.Mediator.Cortex.Configuration;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;
using CryoVigil.API.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using CryoVigil.API.Shared.Infrastructure.Pipeline.Middleware.Components;
using CryoVigil.API.Shared.Interfaces.Rest.ProblemDetails;
using CryoVigil.API.Shared.Resources;
using CryoVigil.API.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
	.AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAllPolicy",
		policy => policy.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
	var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
	if (string.IsNullOrWhiteSpace(connectionStringTemplate))
		throw new InvalidOperationException("Database connection string is not set in the configuration.");

	var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
	if (string.IsNullOrWhiteSpace(connectionString))
		throw new InvalidOperationException("Database connection string is not set in the configuration.");

	options.UseMySQL(connectionString)
		.UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
		.AddInterceptors(serviceProvider.GetRequiredService<DispatchDomainEventsInterceptor>())
		.EnableDetailedErrors();

	if (builder.Environment.IsDevelopment())
		options.EnableSensitiveDataLogging();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();

builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddOpenApi();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<DispatchDomainEventsInterceptor>();
builder.Services.AddScoped<IGeneralSettingsRepository, GeneralSettingsRepository>();
builder.Services.AddScoped<IGeneralSettingsQueryService, GeneralSettingsQueryService>();
builder.Services.AddScoped<IGeneralSettingsCommandService, GeneralSettingsCommandService>();
builder.Services.AddScoped<ISettingsQueryService, SettingsQueryService>();
builder.Services.AddScoped<ISettingsCommandService, SettingsCommandService>();
builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddScoped<IUserProfileQueryService, UserProfileQueryService>();
builder.Services.AddScoped<IUserProfileCommandService, UserProfileCommandService>();
builder.Services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
builder.Services.AddScoped<ILaboratoriesQueryService, LaboratoriesQueryService>();
builder.Services.AddScoped<ILaboratoriesCommandService, LaboratoriesCommandService>();

// Alerts Bounded Context
builder.Services.AddScoped<IAlertRepository, AlertRepository>();
builder.Services.AddScoped<IAlertsQueryService, AlertsQueryService>();
builder.Services.AddScoped<IAlertsCommandService, AlertsCommandService>();

// History Bounded Context
builder.Services.AddScoped<IHistoryEventRepository, HistoryEventRepository>();
builder.Services.AddScoped<IHistoryEventQueryService, HistoryEventQueryService>();
builder.Services.AddScoped<IHistoryEventCommandService, HistoryEventCommandService>();

// Mediator pipeline behaviors
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(ExceptionHandlingCommandBehavior<>));
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(TransactionCommandBehavior<>));

builder.Services.AddCortexMediator([typeof(Program)]);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var context = services.GetRequiredService<AppDbContext>();
	context.Database.Migrate();
}

app.UseExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
	.SetDefaultCulture(supportedCultures[0])
	.AddSupportedCultures(supportedCultures)
	.AddSupportedUICultures(supportedCultures);
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
	app.MapScalarApiReference();
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();