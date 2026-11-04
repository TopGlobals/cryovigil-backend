# Changelog

All notable changes to the CryoVigil API are documented in this file.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [v0.1.0] - 2026-11-04

### Added

- Added the Alerts domain model, including the `Alert` aggregate and
  `AlertSeverity` and `AlertStatus` value objects.
- Added Entity Framework Core configuration for persisted alerts, including
  required fields, length constraints, and generated identifiers.
- Added alert repository abstractions and an Entity Framework Core repository
  implementation.
- Added a query service for retrieving alerts by laboratory ID.
