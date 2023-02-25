using MediatR;
using MyReminder.Application.Messaging;

namespace MyReminder.Infrastructure.MediatR;

public sealed class QueryBus : IQueryBus
{
    private readonly IMediator _mediator;

    public QueryBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<TResponse> QueryAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default) =>
        await _mediator
            .Send(query, cancellationToken);
}
