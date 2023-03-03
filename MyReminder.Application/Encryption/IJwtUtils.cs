using MyReminder.Domain.User.Entities;

namespace MyReminder.Application.Encryption;

public interface IJwtUtils
{
    public string GenerateToken(Domain.User.Entities.User user);
    public string? ValidateToken(string? token);
    public RefreshToken GenerateRefreshToken(string idAddress);
}
