using Microsoft.AspNetCore.Mvc;
using MyReminder.Application.Messaging;
using MyReminder.Domain.User.Entities;

namespace MyReminder.API.Controllers;

public abstract class Controller : ControllerBase
{
    // returns the current authenticated account (null if not logged in)
    public User Account => (User)HttpContext.Items["User"];

    protected const string RoutePattern = "api/v{version:apiVersion}";
    protected const string DefaultApiVersion = "1.0";
    protected Controller(ICommandBus commandBus, IQueryBus queryBus)
    {
        CommandBus = commandBus;
        QueryBus = queryBus;
    }

    protected ICommandBus CommandBus { get; }
    protected IQueryBus QueryBus { get; }
}
