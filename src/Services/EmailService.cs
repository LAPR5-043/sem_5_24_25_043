using System.Net;
using System.Net.Mail;

public class EmailService : IEmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string senderEmail;
        private readonly string senderPassword;

        public EmailService()
        {
            // Configuration for the sender email
            smtpServer = "smtp.office365.com";
            smtpPort = 587;
            senderEmail = "jobs4umanagement@outlook.com";
            senderPassword = "Japan123";
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