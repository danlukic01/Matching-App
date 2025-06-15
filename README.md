# Matching-App

This repository contains a starter ASP.NET Core Web API for a matchmaking application. The API uses Entity Framework Core with SQL Server.

## Building

Ensure the .NET SDK 7.0 or later is installed. From the `src/MatchingApp.Api` directory run:

```
dotnet restore
```

To run the API locally:

```
dotnet run
```

The API exposes endpoints under `/api/clients`:

- `GET /api/clients` – list clients with optional `search`, `page`, and `pageSize` query parameters
- `GET /api/clients/{id}` – retrieve a single client
- `POST /api/clients` – create a client
- `PUT /api/clients/{id}` – update a client
- `DELETE /api/clients/{id}` – remove a client

The API exposes matchmaking endpoints under `/api/matches`:
- `GET /api/matches` – list all matches
- `GET /api/matches/{id}` – retrieve a single match
- `POST /api/matches` – create a match between two clients by providing their `clientAId` and `clientBId`

## Web UI

A simple HTML interface is served from the `wwwroot` folder of `MatchingApp.Api`.
When running the application, navigate to `https://localhost:5001/` (or the configured base URL) to access `index.html` for creating and retrieving clients using the API.
