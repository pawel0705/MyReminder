using MyReminder.API.Authorization;
using MyReminder.Application;
using MyReminder.Application.Encryption;
using MyReminder.Application.Messaging;
using MyReminder.Domain.Contracts;
using MyReminder.Infrastructure.MediatR;
using MyReminder.Infrastructure.Persistence;
using MyReminder.Infrastructure.User.Repositories;

namespace MyReminder.API;

public static class RegisterDependencyInjectionConfiguration
{
    public static IServiceCollection RegisterDependencyInjection(
        this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        var services = serviceCollection
           .RegisterMyReminderInfrastructureDependencies(configuration)
           .RegisterMyReminderApplicationDependencies()
           .AddScoped<ICommandBus, CommandBus>()
           .AddScoped<IQueryBus, QueryBus>()
           .AddScoped<IJwtUtils, JwtUtils>()
           .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
