public interface IEmailService
{
    Task SendConfirmationEmail(string patientEmail, string email);
    Task SendPendingRequestEmail(string patientEmail, string subject, string url);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
    Task SendEmailChangedData(string patientEmail, string subject,List<string> dataChanged);
    Task SendEmailToStaffSignIn(string email, string password);
    Task SendDeletionConfirmationEmail(string email, string url);
    Task SendEmailResetPassword(string email);
}