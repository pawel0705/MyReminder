# EF Migrations

#### Commands to manage migrations:

Uruchamiać z głównego poziomu folderu

```
dotnet ef migrations add Initial -p ./MyReminder.Infrastructure/MyReminder.Infrastructure.csproj --startup-project ./MyReminder/MyReminder.API.csproj --context MyReminderContext
dotnet ef migrations remove -p ./MyReminder.Infrastructure/MyReminder.Infrastructure.csproj --startup-project ./MyReminder/MyReminder.API.csproj --context MyReminderContext
dotnet ef database update -p ./MyReminder.Infrastructure/MyReminder.Infrastructure.csproj --startup-project ./MyReminder/MyReminder.API.csproj --context MyReminderContext
```
