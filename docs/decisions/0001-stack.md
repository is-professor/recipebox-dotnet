# 0001. Stack for the lab app

- **Status:** accepted
- **Date:** 2026-10-15
- **Deciders:** USER

## Context

Recipe Box needs a backend with a REST API (week 4), authentication (week 5), tests and Docker
(week 6), cloud deployment (week 8) and LLM calls (week 10). I know C# from previous courses and
some JavaScript. I work alone and have about 60–90 minutes per week for the challenges, so the
stack must have good tooling and documentation, and I want to reuse it in the team project.

## Options considered

1. **ASP.NET Core (.NET 10) + Razor Pages + EF Core**: I know C#; Razor Pages gives a server-rendered
   web UI and Minimal APIs a REST API in the same project; EF Core has migrations; first-class Azure
   support. Con: more ceremony than Python for small things.
2. **Node.js (TypeScript) + Express + React + Prisma**: one language for front end and back end,
   huge ecosystem. Con: two projects (API + SPA) to build and deploy; I'm weaker in TypeScript;
   many library choices to make myself.
3. **Python + Django**: batteries included (ORM, admin, auth, migrations). Con: I've never used
   Python for web; REST API needs Django REST Framework on top.

## Decision

**ASP.NET Core with Razor Pages and EF Core**, database **SQLite** for local development
(no server to install). I'll switch to **PostgreSQL** when adding Docker in week 6, so local and
cloud use the same database engine.

## Consequences

- Web UI and API live in one project and share the service layer.
- SQLite has limitations (e.g. `decimal` is stored as text, no `ALTER COLUMN`); I have to
  watch for them until the switch to PostgreSQL. EF Core migrations generated for SQLite will
  have to be regenerated for PostgreSQL.
- Server-rendered pages mean the mobile app (week 9) must use the API, not the pages.
