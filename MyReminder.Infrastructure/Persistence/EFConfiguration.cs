using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MyReminder.Infrastructure.Persistence;

public static class EFConfiguration
{
    private const string MigrationsTableName = "__EFMigrationsHistory";
    private const string SqlServerTag = "SqlServerConnection";

    public static IServiceCollection ConfigureDBConnection<TContext>(
        this IServiceCollection serviceCollection,
        IConfiguration configuration,
        string connectionStringSectionName,
        string migrationAssemblyName,
        string migrationTableSchema)
        where TContext : DbContext
    {
        var connectionString = configuration.GetConnectionString(connectionStringSectionName)!;

        Action<DbContextOptionsBuilder> databaseContextOptionsAction = optionsBuilder =>
            optionsBuilder.UseSqlServer(connectionString, sqlServerOptionsBuilder =>
            {
                sqlServerOptionsBuilder.MigrationsAssembly(migrationAssemblyName);
                sqlServerOptionsBuilder.ApplyMigrationsSchemaNameChange(migrationTableSchema);
            });

        return serviceCollection
            .AddDbContext<TContext>(databaseContextOptionsAction)
            .AddHealthChecks()
            .AddSqlServer(
                connectionString,
                healthQuery: "select 1",
                failureStatus: HealthStatus.Degraded,
                name: typeof(TContext).FullName,
                tags: new[] { SqlServerTag, })
            .Services;
    }

    private static SqlServerDbContextOptionsBuilder ApplyMigrationsSchemaNameChange(
        this SqlServerDbContextOptionsBuilder sqlServerOptionsBuilder, string schemaName)
    {
        sqlServerOptionsBuilder
            .MigrationsHistoryTable(MigrationsTableName, schemaName);

        return sqlServerOptionsBuilder;
    }

    public static IApplicationBuilder UseMigrationsOfContext<TContext>(this IApplicationBuilder applicationBuilder) where TContext : DbContext
    {
        using var serviceScope = applicationBuilder
            .ApplicationServices
            .CreateScope();

        var databaseContext = serviceScope
            .ServiceProvider
            .GetService<TContext>();

        var isInvalid = databaseContext is null;

        if (isInvalid)
        {
            var errorMessage =
                $"{nameof(EFConfiguration)}.{nameof(UseMigrationsOfContext)}: {typeof(TContext).FullName}";

            throw new ArgumentException(errorMessage);
        }
        // TODO odkomentowac
        /*
        databaseContext?
            .Database
            .Migrate();
        */
        return applicationBuilder;
    }
}
