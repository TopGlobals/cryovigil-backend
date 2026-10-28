# CryoVigil Web Services API

[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![REST API](https://img.shields.io/badge/API-RESTful-blue.svg)](https://swagger.io/)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

Backend RESTful API and web services for **CryoVigil**, the laboratory asset monitoring platform by **TopGlobals**.

---

## 📋 Table of Contents

- [About the Project](#about-the-project)
- [Architecture & Tech Stack](#architecture--tech-stack)
- [Getting Started](#getting-started)
- [API Documentation](#api-documentation)
- [Git Workflow & Branching Strategy](#git-workflow--branching-strategy)
- [Team Members](#team-members)

---

## 🚀 About the Project

CryoVigil is an automated critical laboratory asset surveillance system designed to monitor ultra-low temperature freezers, incubators, and storage units in real time.

This repository houses the core Web Services API responsible for:
- Data ingestion and real-time asset telemetry tracking.
- System user authentication, authorization, and tenant access control.
- Centralized alerts and regulatory compliance logging.
- Exposing RESTful endpoints for consumption by the CryoVigil Web Application.

---

## 🛠️ Architecture & Tech Stack

This project is built following **Clean Architecture** principles to promote separation of concerns, maintainability, and testability.

- **Framework:** ASP.NET Core Web API (.NET 8.0)
- **Language:** C# 12
- **Persistence / ORM:** Entity Framework Core
- **Documentation:** OpenAPI / Swagger UI
- **IDE:** JetBrains Rider / Visual Studio 2022

### Directory Structure

```text
[TBD]
CryoVigil.API/
```
## 🏁 Getting Started
### 1\. Restore Dependencies

Run the following command to restore the required .NET dependencies:

```
dotnet restore
```

### 2\. Configure Environment Variables

Update `appsettings.Development.json` with your local database connection string if you are connecting to a local database instance.

### 3\. Run the Application

Start the API using the following command:

```
dotnet run --project CryoVigil.API
```

### 4\. Access Swagger UI

Open your browser and navigate to:

- **Swagger UI:** https://localhost:7123/swagger
- **Local OpenAPI Endpoint:** https://localhost:7123/swagger/v1/swagger.json

> **Note:** The port may differ depending on the configuration specified in your console output.

## 📖 API Documentation

Interactive API documentation is automatically generated using the OpenAPI/Swagger middleware.

When running locally or in a staging environment, developers can use the Swagger interface to:

- Test API endpoints interactively.
- Inspect request and response payload DTOs.
- Review response schemas and data structures.
- Validate API behavior during development and testing.

**Staging API Deployment:** `[]`

## 🌿 Git Workflow & Branching Strategy

To maintain code quality and support systematic team collaboration, this project follows a **Feature Branch Workflow**.

### Branch Structure

| Branch | Description |
| --- | --- |
| `main` | Production-ready code and releases. |
| `develop` | Integration branch for completed features. |
| `feature/<feature-name>` | Isolated branches for new user stories or technical tasks. |


## 👥 Team Members — TopGlobals

| Name | Role                        | GitHub Username |
| --- |-----------------------------| --- |
| Arizabal Condori, Jean Niels | Leader                      | @JeanArizabal |
| Homola, Tomas | Developer                   | @Ch1L1 |
| Padilla Merino, Mauricio Jared | Developer         | @ClifeK |
| Reyes Munoz, Joaquin Leonardo | Developer | @JoakoRM |