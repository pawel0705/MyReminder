namespace MyReminder.Application.Messaging;

public interface ICommandBus
{
    Task SendAsync(ICommand command, CancellationToken cancellationToken = default);
    Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);
}
