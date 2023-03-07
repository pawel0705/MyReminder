using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyReminder.Application.Encryption;
using MyReminder.Application.Helpers;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.Models;
using MyReminder.Domain.User.ValueObjects;
using MyReminder.Infrastructure.Persistence;
using System.Security.Cryptography;

namespace MyReminder.Infrastructure.User.Repositories;

public class UserRepository : Repository<Domain.User.Entities.User, UserId>, IUserRepository
{
    private MyReminderContext _context;
    private IJwtUtils _jwtUtils;
    private readonly Settings _settings;

    public UserRepository(
        MyReminderContext context,
        IJwtUtils jwtUtils,
        IOptions<Settings> appSettings)
        : base(context)
    {
        _context = context;
        _jwtUtils = jwtUtils;
        _settings = appSettings.Value;
    }

    public async Task<AuthenticateResponse> Authenticate(Login login, Password password)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.Login == login);

        if (user == null)
        {
            // TODO
        }

        if (!BCrypt.Net.BCrypt.Verify(password.Value, user.PasswordHash.Value))
        {
            // TODO
        }

        var generatedToken = _jwtUtils.GenerateJwtToken(user);

        var response = new AuthenticateResponse(
            user.Id.Id.ToString(),
            user.Login.Value,
            generatedToken);

        return response;
    }

    public async Task<Domain.User.Entities.User> GetById(UserId userId)
    {
        var user = await _context.Users.FindAsync(userId);
        // TODO exception if not found
        return user;
    }

    public async Task<Domain.User.Entities.User> GetByEmail(Email email)
    {
        var user = await _context.Users.Where(x => x.Email == email).SingleOrDefaultAsync();
        // TODO exception if not found
        return user;
    }

    public async Task<Domain.User.Entities.User> GetByLogin(Login login)
    {
        var user = await _context.Users
            .Where(x => x.Login == login)
            .SingleOrDefaultAsync();
        // TODO exception if not found
        return user;
    }

    public async Task<Domain.User.Entities.User> GetByRefreshToken(Token token)
    {
        var user = await _context.Users
            .Include(x => x.RefreshTokens)
            .Where(x => x.RefreshTokens.Any(y => y.Token == token))
            .SingleOrDefaultAsync();

        // TODO if user is null
        return user;
    }

    public async Task UpdateRefreshTokens(UserId userId, Domain.User.Entities.RefreshToken newRefreshToken)
    {
        var user = await _context.Users
            .Include(x => x.RefreshTokens)
            .Where(x => x.Id == userId)
            .SingleOrDefaultAsync();

        user.UpdateRefreshTokens(newRefreshToken, _settings.RefreshTokenTimeToLive);

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task VerifyUser(VerificationToken verificationToken)
    {
        var user = await _context.Users.Where(x => x.VerificationToken == verificationToken).SingleOrDefaultAsync();
        // TODO exception if not found
        if (user is null)
        {
            // exception
        }

        user.Verify();

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> Register(Login login, Email email, Password password)
    {
        if (await _context.Users.AnyAsync(x => x.Login == login))
        {
            return false; // TODO some error/more information
        }

        if (await _context.Users.AnyAsync(x => x.Email == email))
        {
            return false; // TODO
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password.Value);

        var user = new Domain.User.Entities.User(
            new UserId(Guid.NewGuid()),
            login,
            email,
            new PasswordHash(hashedPassword),
            Role.BasicUser,
            GenerateVerificationToken());

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return true;
    }

    private VerificationToken GenerateVerificationToken()
    {
        // token is a cryptographically strong random sequence of values
        var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

        var verificationToken = new VerificationToken(token);

        var tokenIsUnique = !_context.Users.Any(x => x.VerificationToken == verificationToken);

        if (tokenIsUnique is false)
        {
            return GenerateVerificationToken();
        }

        return verificationToken;
    }

    public async Task<Domain.User.Entities.User> GetByResetToken(ResetToken resetToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(x =>
            x.ResetToken == resetToken && x.ResetTokenExpires > DateTime.UtcNow);

        return user;
    }
}
