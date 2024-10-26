using System.Net;
using System.Web;
using MimeKit;
using MailKit.Net.Smtp;

namespace src.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEncryptionEmailService encryptionEmailService;
        private readonly string signatureImageUrl = "https://imgur.com/BB7Qk6j.png";
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string senderEmail;
        private readonly string senderPassword;
        private readonly string apiEndpointUrl;
        private readonly string uiUrl;

        public EmailService(IConfiguration configuration, IEncryptionEmailService encryptionEmailService)
        {
            this.encryptionEmailService = encryptionEmailService;
            

            // Read configuration for the sender email from appsettings.json
            smtpServer = configuration["EmailSettings:SmtpServer"];
            smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
            senderEmail = configuration["EmailSettings:SenderEmail"];
            senderPassword = configuration["EmailSettings:SenderPassword"];
            apiEndpointUrl = configuration["ApiEndpointConfirmationEmail"];
            uiUrl = configuration["HostedUI"];
        }

        public async Task SendConfirmationEmail(string patientEmail, string iamEmail)
        {
            // Encrypt the email
            var encryptedEmail = encryptionEmailService.EncryptEmail(iamEmail);

            // Build the confirmation link with the encrypted email
            var confirmationUrl = $"{apiEndpointUrl}?email={HttpUtility.UrlEncode(encryptedEmail)}";
            
            var message = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background-color: #007bff;
                            color: #ffffff;
                            padding: 10px 0;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                            color: #000000;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 10px 20px;
                            font-size: 16px;
                            color: #ffffff;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                            margin-top: 20px;
                            text-align: center;
                        }}
                        .button-container {{
                            text-align: center;
                        }}
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            color: #888888;
                            font-size: 12px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Email Confirmation</h2>
                        </div>
                        <div class='content'>
                            <p>Dear User,</p>
                            <p>Thank you for registering. Please click the button below to confirm your email address:</p>
                            <div class='button-container'>
                                <a href='{confirmationUrl}' class='button'>Confirm Email</a>
                            </div>
                            <p>If you did not request this email, please ignore it.</p>
                            <br>
                            <p>Best regards,</p>
                            <p>Medopt Team</p>
                            <img src='{signatureImageUrl}' alt='Signature' style='display:block; margin-top:20px;' />
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 Medopt. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";

            // Send the email
            await SendEmailAsync(patientEmail, "Confirm your email", message);
        }
        public async Task SendPendingRequestEmail(string patientEmail, string subject, string url)
        {
        
            
            var message = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background-color: #007bff;
                            color: #ffffff;
                            padding: 10px 0;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                            color: #000000;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 10px 20px;
                            font-size: 16px;
                            color: #ffffff;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                            margin-top: 20px;
                            text-align: center;
                        }}
                        .button-container {{
                            text-align: center;
                        }}
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            color: #888888;
                            font-size: 12px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Change Profile Confirmation</h2>
                        </div>
                        <div class='content'>
                            <p>Dear User,</p>
                            <p>It seems you have been making some changes on your profile</p>
                            <p>Please click the button below to confirm your profile changes:</p>
                            <div class='button-container'>
                                <a href='{url}' class='button'>Confirm Profile Changes</a>
                            </div>
                            <p>If you did not request this email, please ignore it.</p>
                            <br>
                            <p>Best regards,</p>
                            <p>Medopt Team</p>
                            <img src='{signatureImageUrl}' alt='Signature' style='display:block; margin-top:20px;' />
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 Medopt. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";

            // Send the email
            await SendEmailAsync(patientEmail, subject, message);
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Medopt Team", senderEmail));
                message.To.Add(new MailboxAddress("", recipientEmail));
                message.Subject = subject;

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body
                };

                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtpServer, smtpPort, true);
                    await client.AuthenticateAsync(senderEmail, senderPassword);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while sending email to {recipientEmail}: {ex.Message}");
                throw new InvalidOperationException("Failed to send email due to an unexpected error.", ex);
            }
        }

        public Task SendEmailChangedData(string patientEmail, string subject,List<string> dataChanged)
        {
            var message = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background-color: #007bff;
                            color: #ffffff;
                            padding: 10px 0;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                            color: #000000;
                        }}
                    
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            color: #888888;
                            font-size: 12px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Change Profile Confirmation</h2>
                        </div>
                        <div class='content'>
                            <p>Dear User,</p>
                            <p>This email is purposed to alert you that an admin made changes to your data</p>
                            <p>Please contact us if you dont agree with the changes made.</p>
                            <p>Changes made:</p>
                            <ul>
                                {string.Join("", dataChanged.Select(data => $"<li>{data}</li>"))}
                            </ul>
                            <br>
                            <p>Best regards,</p>
                            <p>Medopt Team</p>
                            <img src='{signatureImageUrl}' alt='Signature' style='display:block; margin-top:20px;' />
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 Medopt. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
            
            return SendEmailAsync(patientEmail, subject, message);

        }

        public Task SendEmailToStaffSignIn(string email, string password)
        {
           
            var subject = "Change your credentials";
             var message = $@"
                <html>
                <head>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            background-color: #f4f4f4;
                            margin: 0;
                            padding: 0;
                        }}
                        .container {{
                            width: 100%;
                            max-width: 600px;
                            margin: 0 auto;
                            background-color: #ffffff;
                            padding: 20px;
                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        }}
                        .header {{
                            background-color: #007bff;
                            color: #ffffff;
                            padding: 10px 0;
                            text-align: center;
                        }}
                        .content {{
                            padding: 20px;
                            color: #000000;
                        }}
                        .button {{
                            display: inline-block;
                            padding: 10px 20px;
                            font-size: 16px;
                            color: #ffffff;
                            background-color: #007bff;
                            text-decoration: none;
                            border-radius: 5px;
                            margin-top: 20px;
                            text-align: center;
                        }}
                        .button-container {{
                            text-align: center;
                        }}
                        .footer {{
                            margin-top: 20px;
                            text-align: center;
                            color: #888888;
                            font-size: 12px;
                        }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>Change Profile Confirmation</h2>
                        </div>
                        <div class='content'>
                            <p>Dear User,</p>
                            <p>It seems that your user account was created with the following temporary credentials:</p>
                            <p>Email: {email}</p>
                            <p>Password: {password}</p>
                            <p>In order to gain access, please click in the button bellow to change them:</p>
                            <div class='button-container'>
                                <a href='{uiUrl}' class='button'>Finish Setup</a>
                            </div>
                            <br>
                            <p>Best regards,</p>
                            <p>Medopt Team</p>
                            <img src='{signatureImageUrl}' alt='Signature' style='display:block; margin-top:20px;' />
                        </div>
                        <div class='footer'>
                            <p>&copy; 2024 Medopt. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";
             return SendEmailAsync(email, subject, message);
        }
    }
}