dotnet ef database update --project chat-services
dotnet ef database update --context PersistedGrantDbContext --project identity-server
dotnet ef database update --context MyKeysContext --project identity-server
dotnet ef database update --context ConfigurationDbContext --project identity-server
dotnet ef database update --context ApplicationDbContext --project identity-server