# CarServiceManager

A personal web application that assists users in keeping a record of their vehicle service history and setting up future car service reminders.

## Overview

CarServiceManager is an ASP.NET Core Razor Pages application designed to help vehicle owners manage their vehicle maintenance information.

The application provides a central place to:

* Manage vehicles
* Record vehicle maintenance and service history
* Track vehicle mileage
* Record maintenance expenses
* View upcoming service requirements
* Set future service reminders

## Technology Stack

* ASP.NET Core Razor Pages
* .NET 10
* C#
* Entity Framework Core
* SQL Server
* Tailwind CSS
* JavaScript
* Docker
* Docker Compose
* Azure

## Development

The application supports two development modes.

### HTTPS Debugging

For normal development and debugging, run the application through Visual Studio using the **HTTPS** profile.

Press:

```text
F5
```

This is the primary development and debugging environment.

### Docker Testing

Docker is used to test the application in a containerized environment.

The Docker environment consists of:

```text
CarServiceManager
       │
       ▼
Application Container
       │
       ▼
SQL Server Container
       │
       ▼
MaintenanceManager
```

Docker testing allows the application to be tested using the same containerized setup that will be used for deployment.

## Configuration

The database connection string is **not stored in `appsettings.json`**.

`appsettings.json` contains only the general application configuration:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Database configuration is supplied through environment variables.

## Environment Variables

Create a `.env` file in the same directory as `docker-compose.yml`.

The `.env` file contains the SQL Server credentials required by Docker Compose.

The required variable is:

```text
SA_PASSWORD
```

Do not commit the `.env` file to source control.

A `.env.example` file can be committed to show the required configuration without containing any credentials.

```text
SA_PASSWORD=
```

## Docker Configuration

Docker Compose runs two containers:

```text
carservicemanager
carservicemanager-db
```

The application connects to SQL Server using the Docker service name:

```text
sqlserver,1433
```

SQL Server is exposed to the host machine on:

```text
localhost,1433
```

### Connection Differences

From the application container:

```text
sqlserver,1433
```

From Windows / SSMS:

```text
localhost,1433
```

The application container must use `sqlserver` rather than `localhost` when connecting to SQL Server.

## Running Docker

From the project directory:

```bash
docker compose up --build
```

To run in detached mode:

```bash
docker compose up --build -d
```

Check container status:

```bash
docker compose ps
```

View application logs:

```bash
docker compose logs -f carservicemanager
```

View SQL Server logs:

```bash
docker compose logs -f sqlserver
```

Stop the containers:

```bash
docker compose stop
```

Restart the containers:

```bash
docker compose restart
```

Remove the containers:

```bash
docker compose down
```

## Database

The Docker SQL Server instance uses SQL Server Authentication.

The database is:

```text
MaintenanceManager
```

SQL Server is accessible from the host through:

```text
localhost,1433
```

### SSMS

To connect to the Docker SQL Server instance using SQL Server Management Studio:

```text
Server: localhost,1433
Authentication: SQL Server Authentication
Login: sa
```

The password is supplied from the local `.env` configuration.

If SSMS reports a certificate trust error, enable **Trust server certificate** in the connection options.

## Database Persistence

SQL Server uses a Docker volume to persist database data:

```text
sqlserver_data
```

This allows the database to persist when the containers are stopped or recreated.

Avoid using:

```bash
docker compose down -v
```

unless you intentionally want to remove the Docker database volume and its data.

## LocalDB

LocalDB is not used by the Docker environment.

LocalDB is Windows-specific and cannot run inside the Linux SQL Server container.

The Docker environment therefore uses a regular SQL Server container:

```text
mcr.microsoft.com/mssql/server:2022-latest
```

## Docker Image

The application is built using the project's `Dockerfile`.

To build the image:

```bash
docker compose build
```

To build and run the complete environment:

```bash
docker compose up --build
```

## Recommended Development Workflow

### Application Development

Use Visual Studio with the HTTPS profile:

```text
Visual Studio
      ↓
HTTPS
      ↓
CarServiceManager
```

Use this environment for:

* Writing code
* Debugging
* Breakpoints
* Testing Razor Pages
* Testing application logic

### Container Testing

After making changes, test the application using Docker:

```bash
docker compose up --build
```

This verifies:

* Docker image builds correctly
* Application starts inside the container
* Environment variables are loaded correctly
* Application can connect to SQL Server
* Database connectivity works
* Application works in the containerized environment

## Git

The following files should be committed:

```text
Dockerfile
docker-compose.yml
.env.example
.gitignore
README.md
```

The following file must **not** be committed:

```text
.env
```

The `.env` file contains local credentials and must remain outside source control.

## Production

Production uses Azure infrastructure rather than the local Docker SQL Server container.

Docker is used for local container testing and validation.

Production credentials and other secrets must be supplied through secure environment configuration and must not be committed to source control.

## Author

Bongani Thwala

Personal vehicle maintenance and service tracking application.

https://carservicemanager.azurewebsites.net/
