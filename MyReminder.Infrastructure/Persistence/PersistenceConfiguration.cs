using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MyReminder.Infrastructure.Persistence;

public static class PersistenceConfiguration
{
    private const string MyReminderContextConnectionStringSectionKey = "MyReminderContext";

    public static IServiceCollection RegisterMyReminderContextDependencies(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var services = serviceCollection
            .ConfigureDBConnection<MyReminderContext>(
                configuration,
                MyReminderContextConnectionStringSectionKey,
                Assembly.GetExecutingAssembly().FullName!,
                "MyReminder");

        return services;
    }

    public static IApplicationBuilder UseMyReminderContextMigrations(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMigrationsOfContext<MyReminderContext>();
}
