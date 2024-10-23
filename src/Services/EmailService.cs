using System.Net;
using System.Net.Mail;
using System.Web;

namespace src.Services
{
    public class EmailService : IEmailService
    {

        private readonly IEncryptionEmailService encryptionEmailService;
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string senderEmail;
        private readonly string senderPassword;
        private readonly string apiEndpointUrl;

        public EmailService(IConfiguration configuration, IEncryptionEmailService encryptionEmailService)
        {
            this.encryptionEmailService = encryptionEmailService;

            // Read configuration for the sender email from appsettings.json
            smtpServer = configuration["EmailSettings:SmtpServer"];
            smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            senderEmail = configuration["EmailSettings:SenderEmail"];
            senderPassword = configuration["EmailSettings:SenderPassword"];
            apiEndpointUrl = configuration["ApiEndpointConfirmationEmail"];
        }


        public async Task SendConfirmationEmail(string email)
        {
            // Encrypt the email
            var encryptedEmail = encryptionEmailService.EncryptEmail(email);

            // Build the confirmation link with the encrypted email
            var confirmationUrl = $"{apiEndpointUrl}?email={HttpUtility.UrlEncode(encryptedEmail)}";

            // Send the email (implement your email sending logic here)
            await SendEmailAsync(email, "Confirm your email", $"Click <a href='{confirmationUrl}'>here</a> to confirm your email.");
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            List<string> attachmentPaths = null;
            try
            {
                using (var client = new SmtpClient(smtpServer, smtpPort))
                {
                    client.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(recipientEmail);

                    // Add attachments if any
                    if (attachmentPaths != null)
                    {
                        foreach (var attachmentPath in attachmentPaths)
                        {
                            mailMessage.Attachments.Add(new Attachment(attachmentPath));
                        }
                    }

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }
    }
}