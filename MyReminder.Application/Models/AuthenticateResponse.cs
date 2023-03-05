using MyReminder.Domain.Contracts;
using System.Text.Json.Serialization;

namespace MyReminder.Application.Models;

public class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }
    public string JwtToken { get; set; }

    [JsonIgnore] // refresh token is returned in http only cookie
    public string RefreshToken { get; set; }
}
