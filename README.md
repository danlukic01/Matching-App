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

Additional match related endpoints under `/api/matches`:

- `POST /api/matches` – create a match record between two clients
- `GET /api/matches/recommendations/{clientId}?top=5` – find the top matches for a client. The score is returned on a 0‑10 scale along with text reasons explaining the compatibility.

## Sample Data

For convenience a SQL script with 50 example clients is provided in `scripts/sample-data.sql`.
Execute this file against your SQL Server database to populate the `Clients` and `NatalCharts` tables with test data.



## Web UI

A simple HTML interface is served from the `wwwroot` folder of `MatchingApp.Api`.
When running the application, navigate to `https://localhost:5001/` (or the configured base URL) to access `index.html`.
The page now supports creating clients, fetching individual records, listing all clients, creating matches and finding recommendations.
Natal chart details are shown for each client and match scores are visualised with a progress bar. Recommended matches include short textual reasons describing the compatibility.
