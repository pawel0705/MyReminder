using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using MyReminder.Application.Helpers;

namespace MyReminder.Application.Mail;

public class EmailService : IEmailService
{
    private readonly Settings _settings;

    public EmailService(IOptions<Settings> appSettings)
    {
        _settings = appSettings.Value;
    }

    public void Send(string to, string subject, string html, string from = null)
    {
        // create message
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(from ?? _settings.EmailFrom));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(TextFormat.Html) { Text = html };

        // send email
        using var smtp = new SmtpClient();
        smtp.Connect(_settings.SmtpHost, _settings.SmtpPort, SecureSocketOptions.StartTls);
        smtp.Authenticate(_settings.SmtpUser, _settings.SmtpPass);
        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
