using System.Threading.Tasks;

namespace sem_5_24_25_043
{
    public interface IAuthService
    {
        Task<(string AccessToken, string IdToken)> SignInAsync(string email, string password);
        Task SignUpAsync(string username, string password, string email);
        Task<bool> RegisterNewPatientAsync(string name, string phoneNumber, string email, string patientEmail, string password);
        Task<bool> ConfirmPatientEmailAsync(string encryptedEmail);
        Task<bool> RegisterNewStaffAsync(string iamEmail, string internalEmail, string password, string name, string role, string phoneNumber);
        Task ResetPasswordAsync(string email);
    }
}