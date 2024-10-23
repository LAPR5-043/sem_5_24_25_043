

public interface IEncryptionEmailService
{
    string EncryptEmail(string email);
    string DecryptEmail(string encryptedEmail);
}