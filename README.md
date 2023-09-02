# Simple chat application for self taught .NET tech.

### Demo

- Site: https://hpchatapp.netlify.app
- Accounts: james.bond@gmail.com / john.wick@gmail.com
- Password: 123456

![Alt Text](https://media.giphy.com/media/Iw2NotaxdgtOolwL7z/giphy.gif)

#### Prerequisite

- Postgres
- [.NET Core SDK 3.1.302](https://www.microsoft.com/net/download/all)

#### Steps to run

- Update the connection string in appsettings.json for each service
- Run `dotnet build root.sln` to build whole solution.
- Run `setup.sql` migration script to init databases.
- BE: cd to `identity-server` and `chat-services` then run `dotnet run`
- FE: `cd web && npm install && npm run start`

#### Docker

For testing purpose:

- Backend: Rename file `.env.sample` to `.env` then run `docker-compose up -d`
- Frontend: `cd web && npm install && npm run start`
- Open browser on `http://localhost:8080`

#### Technologies and frameworks used:

- ASP.NET MVC Core 3.1
- Entity Framework Core 3.1
- ASP.NET Identity Core 3.1
- IdentityServer4
- SignalR for notification
- Vue 3

#### Deployment: All services are deployed on free hosting services

- Backend services are hosted on [Fly.io](https://fly.io/)
- Vercel Postgres [Postgres](https://vercel.com/docs/storage/vercel-postgres)
- Frontend app is hosted on [Netlify](https://www.netlify.com/)
