namespace MyReminder.Domain.Models;

public sealed record AuthenticateResponse(
    string Id,
    string Login,
    string Token);
