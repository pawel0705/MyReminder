using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MyReminder.Application;
using MyReminder.Application.Messaging;
using MyReminder.Domain.User;
using MyReminder.Domain.User.Entities;
using MyReminder.Infrastructure.MediatR;
using MyReminder.Infrastructure.Persistence;
using MyReminder.Infrastructure.User.Repositories;
using System.Text;

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
           .AddTransient<IUserRepository, UserRepository>();

        /*
        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
        });
        
        services.AddTransient<SignInManager<User>>();
        services.AddTransient<ISecurityStampValidator, SecurityStampValidator<User>>();
        services.AddTransient<IdentityErrorDescriber>();
        new IdentityBuilder(typeof(User), services).AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
        });

        services
            .AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.SaveToken = true;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Site"],
                    ValidAudience = configuration["Jwt:Site"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]))
                };
            });
        */
        
        /*
        services
                .AddSingleton<IInvalidatedJwtService, InvalidatedJwtService>();
        services
            .AddSingleton<IFailedLoginAttemptService, FailedLoginAttemptService>();

        services
            .AddSingleton<IPostConfigureOptions<JwtBearerOptions>, CustomJwtBearerOptionsPostConfigureOptions>();

        services
            .AddSingleton<CustomJwtSecurityTokenHandler>();

        services
            .AddAuthorization(options =>
            {
                options.AddPolicy("NotBlocked", policy =>
                {
                    policy.AddRequirements(new NotBlockedRequirement());
                });
            });

        services
            .AddScoped<IAuthorizationHandler, ClaimsAuthorizationHandler>();
        services
            .AddScoped<IAuthorizationHandler, NotBlockedAuthorizationHandler>();
        services
            .AddScoped<IAuthorizationHandler, UserTypesAuthorizationHandler>();
        services
            .AddScoped<IAuthorizationHandler, MenuOptionsAuthorizationHandler>();
        services
            .AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
        */
        return services;
    }
}
