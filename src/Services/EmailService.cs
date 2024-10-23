using System.Net;
using System.Net.Mail;
using System.Web;

namespace src.Services
{
    public class EmailService : IEmailService
    {

        private readonly IEncryptionEmailService encryptionEmailService;
        private readonly string bannerImageUrl = "https://imgur.com/a/ODn2ztG";
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
            try
            {
                // Configure the SMTP client for SSL over port X
                SmtpClient client = new SmtpClient(smtpServer, smtpPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true, // SSL is used on port 465
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    IsBodyHtml = true // HTML email content
                };

                mailMessage.To.Add(recipientEmail);

                // Construct HTML body with the banner image link
                string htmlBody = $"<html><body><img src='{bannerImageUrl}' alt='Banner' /><br>{body}</body></html>";

                // Set the email body
                mailMessage.Body = htmlBody;

                // Send the email
                await client.SendMailAsync(mailMessage);
            }
            catch (SmtpException smtpEx)
            {
                // Handle SMTP-specific exceptions
                Console.WriteLine($"SMTP error while sending email to {recipientEmail}: {smtpEx.Message}");
                throw new InvalidOperationException("Failed to send email due to SMTP error.", smtpEx);
            }
            catch (Exception ex)
            {
                // Handle general exceptions
                Console.WriteLine($"General error while sending email to {recipientEmail}: {ex.Message}");
                throw new InvalidOperationException("Failed to send email due to an unexpected error.", ex);
            }
        }


    }
}