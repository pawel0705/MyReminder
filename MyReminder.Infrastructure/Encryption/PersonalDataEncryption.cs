using Microsoft.Extensions.Configuration;
using MyReminder.Application.Encryption;
using System.Security.Cryptography;
using System.Text;

namespace MyReminder.Infrastructure.Encryption;

public class PersonalDataEncryption : IPersonalDataEncryption
{
    private readonly string key;
    private readonly string iv;

    private readonly SymmetricAlgorithm _encryptionAlgorithm;

    public PersonalDataEncryption(IConfiguration configuration)
    {
        var aesSection = configuration.GetSection("AES");
        key = aesSection.GetSection("Key").Value!;
        iv = aesSection.GetSection("Iv").Value!;
        _encryptionAlgorithm = Aes.Create();
        _encryptionAlgorithm.Key = Convert.FromBase64String(key);
        _encryptionAlgorithm.IV = Convert.FromBase64String(iv);
    }

    public string? Encrypt(string input)
    {
        if (input == null)
        {
            return null;
        }

        var bytes = Encoding.UTF8.GetBytes(input);
        var encryptedBytes = null as byte[];

        var personalDataEncryptor = _encryptionAlgorithm.CreateEncryptor();

        using (var msEncrypt = new MemoryStream())
        {
            using var csEncrypt = new CryptoStream(msEncrypt, personalDataEncryptor, CryptoStreamMode.Write);
            using (var swEncrypt = new BinaryWriter(csEncrypt, Encoding.UTF8))
            {
                swEncrypt.Write(bytes);
            }

            encryptedBytes = msEncrypt.ToArray();
        }

        var encrypted = Convert.ToBase64String(encryptedBytes);
        return encrypted;
    }

    public string? Decrypt(string input)
    {
        if (input == null)
        {
            return null;
        }

        var encryptedBytes = Convert.FromBase64String(input);
        var decrypted = null as string;

        var personalDataDecryptor = _encryptionAlgorithm.CreateDecryptor();

        using (var msDecrypt = new MemoryStream(encryptedBytes))
        {
            using var csDecrypt = new CryptoStream(msDecrypt, personalDataDecryptor, CryptoStreamMode.Read);
            using var srDecryptor = new StreamReader(csDecrypt, Encoding.UTF8);
            decrypted = srDecryptor.ReadToEnd();
        }

        return decrypted;
    }
}
