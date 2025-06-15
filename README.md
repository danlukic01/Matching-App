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

The application will automatically apply any pending Entity Framework
migrations at startup. If you prefer to update the database manually run:

```
dotnet ef database update
```

The API exposes endpoints under `/api/clients`:

- `GET /api/clients` – list clients with optional `search`, `page`, and `pageSize` query parameters
- `GET /api/clients/{id}` – retrieve a single client
- `POST /api/clients` – create a client
- `PUT /api/clients/{id}` – update a client
- `DELETE /api/clients/{id}` – remove a client

Clients now include `gender` and `preferredGender` properties used to filter match recommendations.

Additional match related endpoints under `/api/matches`:

- `POST /api/matches` – create a match record between two clients
- `GET /api/matches/recommendations/{clientId}?top=5` – find the top matches for a client. The score is returned on a 0‑10 scale along with text reasons explaining the compatibility.



## Web UI

A simple HTML interface is served from the `wwwroot` folder of `MatchingApp.Api`.
`index.html` exposes the admin style CRUD forms used for testing the API.
For a cleaner user experience open `app.html` instead – this page lets a user
create their profile and immediately search for compatible matches.
Natal chart details are shown for each match along with a percentage score.
