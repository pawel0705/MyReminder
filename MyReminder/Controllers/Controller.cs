using Microsoft.AspNetCore.Mvc;
using MyReminder.Application.Messaging;

namespace MyReminder.API.Controllers;

public abstract class Controller : ControllerBase
{
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
