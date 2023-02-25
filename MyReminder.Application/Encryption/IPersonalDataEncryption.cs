namespace MyReminder.Application.Encryption;

public interface IPersonalDataEncryption
{
    string? Encrypt(string input);

    string? Decrypt(string input);
}
