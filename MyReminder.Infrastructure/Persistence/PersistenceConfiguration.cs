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
        => serviceCollection
            .ConfigureDatabaseConnection<MyReminderContext>(
                configuration,
                FacilityContextConnectionStringSectionKey,
                Assembly.GetExecutingAssembly().FullName,
                MigrationsSchemaSettingsExtractor.Extract(configuration).FacilityContext);

    public static IApplicationBuilder UseFacilityContextMigrations(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMigrationsOfContext<FacilityContext>();
}
