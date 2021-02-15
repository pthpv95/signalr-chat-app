# Simple chat application for self taught .NET tech.

#### Prerequisite

- Postgres
- [.NET Core SDK 3.1.302](https://www.microsoft.com/net/download/all)

#### Steps to run
- Update the connection string in appsettings.json for each service
- Run `dotnet build root.sln` to build whole solution.
- Run database migrations for services:
  - `dotnet ef database update --project chat-services`
  - `dotnet ef database update --context PersistedGrantDbContext --project identity-server`
  - `dotnet ef database update --context MyKeysContext --project identity-server`
  - `dotnet ef database update --context ConfigurationDbContext --project identity-server`
  - `dotnet ef database update --context ApplicationDbContext --project identity-server`
- BE: cd to `identity-server` and `chat-services` then run `dotnet run`
- FE: `cd web && npm install && npm run start`

#### Docker

For testing purpose:
- Backend: `docker-compose -f docker-compose.dev.yml up`
- Frontend: `cd web && npm install && npm run start`
- Open brower on `http://localhost:8080`

#### Technologies and frameworks used:

- ASP.NET MVC Core 3.1
- Entity Framework Core 3.1
- ASP.NET Identity Core 3.1
- IdentityServer4
- Redis for notification
- Vue 3

#### Deployment:
- All are deployed on free hosting services.
- Backend services are hosted on [Heroku](https://dashboard.heroku.com/)
- Frontend app is deployed and hosted by [netlify](https://www.netlify.com/)
