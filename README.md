# Recipe Box — reference solution (.NET)


## Authors

| Name | GitHub | Main responsibilities |
|------|--------|-----------------------|
| Teaching assistant | @is-professor | Everything |

## Stack

ASP.NET Core 10 (Razor Pages + one Minimal API endpoint), EF Core 10, SQLite.
Reasons: [docs/decisions/0001-stack.md](docs/decisions/0001-stack.md).

## Running locally

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)

Nothing else: the database is a SQLite file that is created on first start.

### Start
```bash
dotnet run --project src/RecipeBox.Web
```
Open http://localhost:5168. On startup the app applies all EF Core migrations, which create
`src/RecipeBox.Web/recipebox.db` with seed data. Delete that file to start over.

| URL | What |
|-----|------|
| `/` | Recipe list, server-rendered (C4, E2) |
| `/Recipes/Details/1` | Recipe detail with ingredients (E1) |
| `/Recipes/ClientSide` | Same list rendered in the browser with `fetch()` (S1) |
| `/api/recipes` | JSON used by the client-side page (S1) |

### Migrations
```bash
dotnet tool restore                      # installs dotnet-ef from .config/dotnet-tools.json
dotnet ef migrations add <Name> --project src/RecipeBox.Web -o Data/Migrations
```

### SQL logging
In Development, every SQL command is logged to the console
(`appsettings.Development.json` → `Microsoft.EntityFrameworkCore.Database.Command`).

## Structure

```
src/RecipeBox.Web/
├── Data/            # entities, DbContext, seed data, migrations   (data access)
├── Services/        # RecipeService: queries and business logic     (business logic)
├── Pages/           # Razor Pages                                    (presentation)
└── Program.cs       # wiring + /api/recipes endpoint
docs/                # data model, decision record
api-tests/           # httpbin.http (C1)
```
