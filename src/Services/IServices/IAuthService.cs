using System.Threading.Tasks;

namespace sem_5_24_25_043
{
    public interface IAuthService
    {
        Task<(string AccessToken, string IdToken)> SignInAsync(string email, string password);
        Task SignUpAsync(string username, string password, string email);
        Task ConfirmSignUpAsync(string username, string confirmationCode);
        Task<string?> GetUserEmailByTokenAsync(string authToken);
        Task<bool> RegisterNewPatientAsync(string name, string phoneNumber, string email, string patientEmail, string password);
        Task<bool> ConfirmPatientEmailAsync(string encryptedEmail);
    }
}