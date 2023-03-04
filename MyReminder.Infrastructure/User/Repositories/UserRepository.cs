using Microsoft.EntityFrameworkCore;
using MyReminder.Application.Encryption;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.Models;
using MyReminder.Domain.User.ValueObjects;
using MyReminder.Infrastructure.Persistence;
using System.Security.Cryptography;

namespace MyReminder.Infrastructure.User.Repositories;

public class UserRepository : IUserRepository
{
    private MyReminderContext _context;
    private IJwtUtils _jwtUtils;

    public UserRepository(
        MyReminderContext context,
        IJwtUtils jwtUtils)
    {
        _context = context;
        _jwtUtils = jwtUtils;
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

        var generatedToken = _jwtUtils.GenerateToken(user);

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
        var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        // TODO exception if not found
        return user;
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
}
