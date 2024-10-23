using System.Security.Cryptography;
using System.Text;

public class EmailEncryptionService : IEncryptionEmailService
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public EmailEncryptionService(IConfiguration configuration)
    {
        var keyString = configuration["EncryptionSettings:Key"]; // Or retrieve from environment or secrets manager
        _key = Encoding.UTF8.GetBytes(keyString.Substring(0, 32)); // Ensure it's 32 bytes
        _iv = Encoding.UTF8.GetBytes(keyString.Substring(32, 16)); // Ensure it's 16 bytes
    }

    public string EncryptEmail(string email)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(email);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    public string DecryptEmail(string encryptedEmail)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = _key;
            aes.IV = _iv;

            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedEmail)))
            {
                using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
    }
}
