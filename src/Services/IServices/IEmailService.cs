public interface IEmailService
    {
    Task SendConfirmationEmail(string email);
    Task SendPendingRequestEmail(string patientEmail, string subject, string url);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
    }