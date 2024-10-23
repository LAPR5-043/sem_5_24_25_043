public interface IEmailService
    {
    Task SendConfirmationEmail(string email);
    Task SendEmailAsync(string email, string subject, string htmlMessage);
    }