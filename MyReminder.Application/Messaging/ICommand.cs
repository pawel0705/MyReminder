using MediatR;

namespace MyReminder.Application.Messaging;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
{
}
