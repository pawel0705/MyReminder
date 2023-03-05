using MyReminder.Domain.User.Entities;

namespace MyReminder.Application.Encryption;

public interface IJwtUtils
{
    public string GenerateJwtToken(Domain.User.Entities.User user);
    public string? ValidateJwtToken(string? token);
    public RefreshToken GenerateRefreshToken(string idAddress);
}
