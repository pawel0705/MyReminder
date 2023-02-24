using MediatR;
using MyReminder.Domain.Common.Events;
using MyReminder.Infrastructure.Persistence;

namespace MyReminder.API;

public static class RegisterDependencyInjectionConfiguration
{
    public static IServiceCollection RegisterDependencyInjection(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
            => serviceCollection
        .RegisterMyReminderInfrastructureDependencies(configuration);
}
