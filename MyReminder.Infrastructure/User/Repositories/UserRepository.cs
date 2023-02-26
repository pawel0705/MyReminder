using Microsoft.EntityFrameworkCore;
using MyReminder.Application.Encryption;
using MyReminder.Domain.Contracts;
using MyReminder.Domain.Entities.User.ValueObjects;
using MyReminder.Domain.Models;
using MyReminder.Infrastructure.Persistence;

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

    public async Task Register(Login login, Email email, Password password)
    {
        if (await _context.Users.AnyAsync(x => x.Login == login))
        {
            // TODO 
        }

        if (await _context.Users.AnyAsync(x => x.Email == email))
        {
            // TODO
        }

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password.Value);

        var user = new Domain.User.Entities.User(
            new UserId(Guid.NewGuid()), 
            login, 
            email, 
            new PasswordHash(hashedPassword));

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
