using MyReminder.Domain.Models;
using MyReminder.Domain.User.ValueObjects;

namespace MyReminder.Domain.Contracts;

public interface IUserRepository
{
    Task<User.Entities.User> GetByEmail(Email email);
    Task<User.Entities.User> GetById(UserId userId);
    Task<User.Entities.User> GetByLogin(Login login);
    Task UpdateRefreshTokens(UserId userId, User.Entities.RefreshToken newRefreshToken);
    Task VerifyUser(VerificationToken verificationToken);
    Task<AuthenticateResponse> Authenticate(Login login, Password password);
    Task<bool> Register(Login login, Email email, Password password);
}
