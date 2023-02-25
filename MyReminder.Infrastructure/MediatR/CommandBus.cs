using MediatR;
using MyReminder.Application.Messaging;

namespace MyReminder.Infrastructure.MediatR;

public sealed class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;

    public CommandBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task SendAsync(ICommand command, CancellationToken cancellationToken = default)
        => await _mediator.Send(command, cancellationToken);

    public async Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default)
        => await _mediator.Send(command, cancellationToken);
}
