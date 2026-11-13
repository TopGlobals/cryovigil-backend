# Changelog

All notable changes to the CryoVigil API are documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [v1.0.0] - 2026-11-12

### Added

- Added Identity and Access Management (IAM) bounded context ported from reference architecture:
  - User aggregate with `UserId` strongly-typed value object and audit timestamps.
  - BCrypt password hashing (`HashingService`) and HMAC-SHA256 JWT bearer token generation (`TokenService`).
  - Custom `RequestAuthorizationMiddleware`, `[Authorize]`, and `[AllowAnonymous]` pipeline attributes.
  - REST endpoints: `AuthenticationController` (`/sign-in`, `/sign-up`), `TokensController` (`/tokens`), and `UsersController` (`/users`).
  - EF Core migration `20261112190000_AddIamContext` creating `users` table.
  - OpenAPI/Scalar Bearer security document and operation transformers.
- Added History bounded context (`OperationEvents`):
  - `HistoryEvent` aggregate and repository for auditing laboratory operational events.
  - Dual routing support for `/api/v1/history` and `/api/v1/history-events`.
  - Migration `20261111120000_AddHistoryContext` with initial seed events.
- Added CRUD endpoints for the global settings resource, including general
  preferences, notifications, security preferences, and monitoring sensors.
- Expanded settings persistence and added a migration that preserves existing
  general settings while adding the new sections.
- Split the current user's identity profile into its own `/api/v1/profiles/current`
  resource, leaving personal preferences and sensor configuration in settings.
- Added a migration that moves existing profile data out of settings without
  losing the saved profile values.
- Added containerization and cloud deployment readiness:
  - Multi-stage `Dockerfile` and `.dockerignore` targeting .NET 10.
  - `render.yaml` Blueprint for Render Web Service deployment.
  - `appsettings.Production.json` supporting environment variable expansion for cloud MySQL (Clever Cloud).

### Fixed

- Fixed MySQL duplicate key error on `pk_settings_sensors` by eager-loading sensors on all settings queries.
- Fixed HTTP 500 runtime casting exception in `BaseRepository` when querying entities with strongly-typed/value-object keys (`AlertId`).
- Fixed case-sensitive enum parsing in `SettingsController` and `GeneralSettingsController` for temperature units.
- Fixed dependency injection scope validation by changing `ProblemDetailsFactory` from Singleton to Scoped.
- Fixed unhandled domain exceptions in `LaboratoriesController` and `UserProfilesController` to return RFC 9457 Problem Details (400 Bad Request) instead of 500.
- Added exponential-backoff retry policy (5 retries, 5s interval) around `context.Database.Migrate()` on startup to tolerate cloud database network latency.

## [v0.1.0] - 2026-11-04

### Added

- Added the Alerts domain model, including the `Alert` aggregate and
  `AlertSeverity` and `AlertStatus` value objects.
- Added Entity Framework Core configuration for persisted alerts, including
  required fields, length constraints, and generated identifiers.
- Added alert repository abstractions and an Entity Framework Core repository
  implementation.
- Added a query service for retrieving alerts by laboratory ID.
