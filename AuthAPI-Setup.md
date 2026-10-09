# AuthAPI Setup and Authentication

This document explains how to set up **AuthAPI** so that login works correctly in **CarServiceManager**.

CarServiceManager uses AuthAPI's authentication endpoints. For login to work, AuthAPI must be running and reachable from CarServiceManager.

## 1. Clone AuthAPI

Clone the AuthAPI repository into a separate directory:

```bash
git clone https://github.com/Bongx514/AuthAPI.git
```

```bash
cd AuthAPI
```

Follow the AuthAPI repository's README for any project-specific setup requirements.

## 2. Configure and start AuthAPI

Configure the environment variables, database connection, and other settings required by AuthAPI. Keep credentials in local environment configuration and do not commit secrets to source control.

From the AuthAPI project directory, build and start its containers:

```bash
docker compose up --build -d
```

Check that the containers are running:

```bash
docker compose ps
```

If the API container is not running as expected, inspect its logs:

```bash
docker compose logs -f
```

Once AuthAPI is running, its configured API endpoints should be available to CarServiceManager.

**Important:** CarServiceManager must use an API base URL that is reachable from the environment where CarServiceManager is running. When an application runs inside a container, `localhost` refers to that same container; it does not automatically refer to the AuthAPI container. For container-to-container communication, configure a reachable Docker service name and port, and ensure the containers share an appropriate Docker network.

## 3. Keep user records and IDs consistent

For the intended authentication workflow, **CarServiceManager and AuthAPI should use the same database and user records**, unless an explicit user-mapping mechanism has been implemented.

This matters because authenticating a user through AuthAPI does not automatically ensure that CarServiceManager can identify the corresponding application user. If the applications use separate databases, the same email address may be associated with different user IDs, or a user may exist in one database but not the other.

### Example: UserId mismatch

Imagine that `test@email.com` exists in both databases:

| Field | AuthAPI database | CarServiceManager database |
|---|---|---|
| Email | `test@email.com` | `test@email.com` |
| UserId | `1` | `3` |

AuthAPI may successfully authenticate the user and return `UserId = 1`. However, CarServiceManager may store that user's application record with `UserId = 3`.

If CarServiceManager uses the API's ID to look up the application user, the lookup may fail or could associate the user with the wrong record. This can prevent the user from logging in or using application features that depend on their user ID, even when their credentials are correct.

### How to prevent a mismatch

- Configure both applications to use the same SQL Server database and the same user records.
- Ensure the user IDs are consistent wherever the applications rely on them.
- If separate databases are intentional, implement an explicit and reliable mapping between AuthAPI users and CarServiceManager users. Do not assume that matching email addresses have matching numeric IDs.

Do not try to fix a mismatch by manually changing IDs without checking foreign-key relationships and dependent records.

## 4. Authentication error handling

CarServiceManager includes error handling intended to explain authentication problems to the user, including cases where the application cannot identify the corresponding user record.

This feedback can help distinguish a problem with the credentials from issues such as an unavailable API or inconsistent user records. It does not remove the need to run AuthAPI or to keep user identity data consistent.

## 5. Recommended local development workflow

1. Clone and configure AuthAPI.
2. Start the AuthAPI containers from the AuthAPI project directory.
3. Confirm that the API container is running and its endpoints are reachable.
4. Confirm that AuthAPI and CarServiceManager use the intended shared database and consistent user records.
5. Run CarServiceManager using its Visual Studio HTTPS profile or start its Docker containers.
6. Test login and review the application/API logs if authentication fails.

Starting CarServiceManager's containers does **not** automatically start AuthAPI when the projects have separate Docker Compose configurations. Start each project separately unless you have deliberately combined their Compose setups.

## Security and configuration notes

- Do not commit `.env` files, database passwords, API secrets, or other credentials.
- Use each project's README for its exact environment variables and configuration.
- In production, make sure CarServiceManager can reach the deployed AuthAPI service and that both applications use a consistent user identity strategy.

## Related project

CarServiceManager is a personal web application for recording vehicle service history and setting future maintenance reminders.

Application: https://carservicemanager.azurewebsites.net/
