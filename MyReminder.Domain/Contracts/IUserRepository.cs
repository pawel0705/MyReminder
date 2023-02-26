using MyReminder.Domain.Entities.User.ValueObjects;
using MyReminder.Domain.Models;

namespace MyReminder.Domain.Contracts;

public interface IUserRepository
{
    Task<User.Entities.User> GetById(UserId userId);
    Task<AuthenticateResponse> Authenticate(Login login, Password password);
    Task Register(Login login, Email email, Password password);
}
