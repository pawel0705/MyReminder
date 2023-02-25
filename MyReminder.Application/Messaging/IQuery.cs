using MediatR;

namespace MyReminder.Application.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}

