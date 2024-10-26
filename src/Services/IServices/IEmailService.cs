public interface IEmailService
    {
    Task SendConfirmationEmail(string patientEmail, string email);
    Task SendPendingRequestEmail(string patientEmail, string subject, string url);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
    Task SendEmailChangedData(string patientEmail, string subject,List<string> dataChanged);
    }