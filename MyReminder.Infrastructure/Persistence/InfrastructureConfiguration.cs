using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace MyReminder.Infrastructure.Persistence;

public static class InfrastructureConfiguration
{
    public static IServiceCollection RegisterMyReminderInfrastructureDependencies(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.RegisterValidators();

        return serviceCollection
            .RegisterMyReminderContextDependencies(configuration);
    }

    private static IServiceCollection RegisterValidators(this IServiceCollection serviceCollection)
        => serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}
