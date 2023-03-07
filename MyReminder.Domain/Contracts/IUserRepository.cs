using MyReminder.Domain.Models;
using MyReminder.Domain.User.Entities;
using MyReminder.Domain.User.ValueObjects;
using System.Net;

namespace MyReminder.Domain.Contracts;

public interface IUserRepository : IRepository<User.Entities.User, UserId>
{
    Task<User.Entities.User> GetByEmail(Email email);
    Task<User.Entities.User> GetById(UserId userId);
    Task<User.Entities.User> GetByLogin(Login login);
    Task<User.Entities.User> GetByRefreshToken(Token refreshToken);
    Task<User.Entities.User> GetByResetToken(ResetToken resetToken);
    Task UpdateRefreshTokens(UserId userId, RefreshToken newRefreshToken);
    Task VerifyUser(VerificationToken verificationToken);
    Task<AuthenticateResponse> Authenticate(Login login, Password password);
    Task<bool> Register(Login login, Email email, Password password);
}
