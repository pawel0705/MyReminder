using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MyReminder.Application;

public static class ApplicationConfiguration
{
    public static IServiceCollection RegisterMyReminderApplicationDependencies(this IServiceCollection serviceCollection)
        => serviceCollection
            .RegisterHandlers()
            .RegisterValidators();

    private static IServiceCollection RegisterHandlers(this IServiceCollection serviceCollection)
        => serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());

    private static IServiceCollection RegisterValidators(this IServiceCollection serviceCollection)
        => serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}
