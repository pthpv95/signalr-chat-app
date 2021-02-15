# Stage 1
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /build
COPY . .
RUN dotnet restore
RUN dotnet publish chat-services.sln -c Release -o /app
# Stage 2
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS final
WORKDIR /app
COPY --from=build /app .
# Comment it when deploying to heroku
ENTRYPOINT ["dotnet", "chat-services.dll"]

# heroku uses the following
# CMD ASPNETCORE_URLS=http://*:$PORT dotnet chat-services.dll