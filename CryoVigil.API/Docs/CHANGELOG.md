# Changelog

All notable changes to the CryoVigil API are documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

- Added CRUD endpoints for the global settings resource, including general
  preferences, notifications, security preferences, and monitoring sensors.
- Expanded settings persistence and added a migration that preserves existing
  general settings while adding the new sections.
- Split the current user's identity profile into its own `/api/v1/profiles/current`
  resource, leaving personal preferences and sensor configuration in settings.
- Added a migration that moves existing profile data out of settings without
  losing the saved profile values.

## [v0.1.0] - 2026-11-04

### Added

- Added the Alerts domain model, including the `Alert` aggregate and
  `AlertSeverity` and `AlertStatus` value objects.
- Added Entity Framework Core configuration for persisted alerts, including
  required fields, length constraints, and generated identifiers.
- Added alert repository abstractions and an Entity Framework Core repository
  implementation.
- Added a query service for retrieving alerts by laboratory ID.
