namespace MyReminder.API.Helpers;

public class Settings
{
    public string Secret { get; set; }
    public int RefreshTokenTimeToLive { get; set; }
    public string EmailFrom { get; set; }
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPass { get; set; }
}
