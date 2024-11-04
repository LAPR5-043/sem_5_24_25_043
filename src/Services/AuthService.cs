using System.Security.Cryptography;
using System.Text;
using Amazon.CognitoIdentityProvider.Model;
using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace sem_5_24_25_043;

public class AuthService : IAuthService
{
    private readonly string _managementId;
    private readonly string _managementSecret;
    private readonly string _audience;
    private readonly string _managementDomain;
    private readonly string _medoptId;
    private readonly string _medoptSecret;
  

    private readonly IEmailService _emailService;
    private readonly IEncryptionEmailService _encryptionEmailService;

    public AuthService() { }

    public AuthService(IConfiguration configuration, IEmailService emailService,
        IEncryptionEmailService encryptionEmailService)
    {
        _managementId = configuration["Auth0:ManagementApi:ClientId"];
        _managementSecret = configuration["Auth0:ManagementApi:ClientSecret"];
        _audience = configuration["Auth0:MedOpt:Audience"];
        _managementDomain = configuration["Auth0:ManagementApi:Domain"];
        _medoptId = configuration["Auth0:MedOpt:ClientId"];
        _medoptSecret = configuration["Auth0:MedOpt:ClientSecret"];
        _emailService = emailService;
        _encryptionEmailService = encryptionEmailService;
    }

    public async Task<(string AccessToken, string IdToken)> SignInAsync(string email, string password)
    {
        
        var client = new AuthenticationApiClient(new Uri($"https://{_managementDomain}"));

        try
        {
            var response = await client.GetTokenAsync(new ResourceOwnerTokenRequest
            {
                ClientId = _medoptId,
                ClientSecret = _medoptSecret,
                Realm = "Username-Password-Authentication",
                Scope = "openid profile email",
                Username = email,
                Password = password,
                Audience = _audience
            });

            return (response.AccessToken, response.IdToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Authentication failed.", ex);
        }
    }


    public async Task SignUpAsync(string email, string password, string name)
    {
       
        return;
    }


    private string CalculateSecretHash(string username)
    {
        // Create the message by concatenating the username and client ID
        var message = username + _managementId;

        // Convert the message and key to byte arrays
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] keyBytes = Encoding.UTF8.GetBytes(_managementSecret);

        // Generate the HMAC SHA256 hash
        using (var hmacsha256 = new HMACSHA256(keyBytes))
        {
            byte[] hashMessage = hmacsha256.ComputeHash(messageBytes);
            // Return the base64-encoded hash
            return Convert.ToBase64String(hashMessage);
        }
    }

  

    public async Task<bool> RegisterNewPatientAsync(string name, string phoneNumber, string email, string patientEmail,
        string password)
    {
        // Initialize the Auth0 Management API client
        var accessToken = await GetAuth0ApiAccessTokenAsync();
        var managementClient = new ManagementApiClient(accessToken, new Uri($"https://{_managementDomain}/api/v2/"));

        // Create user in Auth0
        var newUser = new UserCreateRequest
        {
            Connection = "Username-Password-Authentication",
            Email = email,
            Password = password,
            EmailVerified = true,
            AppMetadata = new
            {
                name,
                phone_number = phoneNumber,
                internalEmail = patientEmail
            },
            Blocked = true
            
        };

        var createdUser = await managementClient.Users.CreateAsync(newUser,CancellationToken.None);

        if (createdUser == null) return false;

        // Assign the user to the specified role
        try
        {
            await managementClient.Users.AssignRolesAsync(createdUser.UserId, new AssignRolesRequest
            {
                Roles = new[] { "rol_NrOguZdRzBqKpjT2" }
            });
        }
        catch (Exception e)
        {
            Console.WriteLine("falhou nos roles burro do caralho");
            throw;
        }


        try
        {
            await _emailService.SendConfirmationEmail(patientEmail, email);
        }
        catch (Exception e)
        {
            Console.WriteLine("falhou no email");
            throw;
        }
       

        return createdUser.UserId != null;
    }

    public async Task<bool> ConfirmPatientEmailAsync(string encryptedEmail)
    {
        var email = _encryptionEmailService.DecryptEmail(encryptedEmail);

        try
        {
            // Initialize the Auth0 Management API client
            var accessToken = await GetAuth0ApiAccessTokenAsync();
            var managementClient = new ManagementApiClient(accessToken, new Uri($"https://{_managementDomain}/api/v2/"));
            // Retrieve the user by email
            var users = await managementClient.Users.GetUsersByEmailAsync(email);
            var user = users.FirstOrDefault();

            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            // Update user to mark email as verified
            var updateRequest = new UserUpdateRequest
            {
                Blocked = false  // Enable the user account after email verification
            };

            await managementClient.Users.UpdateAsync(user.UserId, updateRequest);

            return true;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to confirm email.", ex);
        }
    }

    public async Task<bool> RegisterNewStaffAsync(string iamEmail, string internalEmail, string password, string name, string role, string phoneNumber)
    {
        // Initialize the Auth0 Management API client
        var accessToken = await GetAuth0ApiAccessTokenAsync();
        var managementClient = new ManagementApiClient(accessToken, new Uri($"https://{_managementDomain}/api/v2/"));

        // Create user in Auth0
        var newUser = new UserCreateRequest
        {
            Connection = "Username-Password-Authentication",
            Email = iamEmail,
            Password = password,
            EmailVerified = true,
            AppMetadata = new
            {
                name,
                phone_number = phoneNumber,
                internalEmail = internalEmail
            },
            
        };

       var createdUser = await managementClient.Users.CreateAsync(newUser,CancellationToken.None);

        if (createdUser == null) return false;

        // Assign the user to the specified role
        
        await managementClient.Users.AssignRolesAsync(createdUser.UserId, new AssignRolesRequest
        {
            Roles = new[] { role }
        });

        return createdUser.UserId != null;
    }

    public async Task ForgotPasswordAsync(string email)
    {
        var request = new ForgotPasswordRequest
        {
            ClientId = _managementId,
            Username = email,
            SecretHash = CalculateSecretHash(email)
        };

      //  await _provider.ForgotPasswordAsync(request);
    }

    public async Task ConfirmForgotPasswordAsync(string email, string confirmationCode, string newPassword)
    {
        var request = new ConfirmForgotPasswordRequest
        {
            ClientId = _managementId,
            Username = email,
            ConfirmationCode = confirmationCode,
            Password = newPassword,
            SecretHash = CalculateSecretHash(email)
        };

       // await _provider.ConfirmForgotPasswordAsync(request);
    }

    public async Task ResetPasswordAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty");
        }
       
        if (!email.Contains("@"))
        {
            throw new ArgumentException("Email must contain @");
        }
       
        var atIndex = email.IndexOf("@");
        if (atIndex == email.Length - 1 || !email.Substring(atIndex).Contains("."))
        {
            throw new ArgumentException("Email must contain a dot after @");
        }
       
        // Check if the email exists in the IAM
        var accessToken = await GetAuth0ApiAccessTokenAsync();
        var managementClient = new ManagementApiClient(accessToken, new Uri($"https://{_managementDomain}/api/v2/"));
        var response = await managementClient.Users.GetUsersByEmailAsync(email);
        
       
        if (response.Count == 0)
        {
            throw new InvalidOperationException("Email not registered to a user.");
        }
       
        // If email exists, send the reset password email
        await _emailService.SendEmailResetPassword(email);
    }
    
  
    private async Task<string> GetAuth0ApiAccessTokenAsync()
    {
        string filePath = "managementToken.csv";
        string token = null;
        DateTime tokenExpiration = DateTime.MinValue;
        DateTime tokenCreationTime = DateTime.MinValue;

        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            if (lines.Length > 0)
            {
                var data = lines[0].Split(',');
                token = data[0];
                DateTime.TryParse(data[1], out tokenExpiration);
                DateTime.TryParse(data[2], out tokenCreationTime);
            }
        }

        if (string.IsNullOrEmpty(token) || DateTime.UtcNow >= tokenExpiration)
        {
            try
            {
                var authenticationApiClient = new AuthenticationApiClient(_managementDomain);
                var tokenRequest = new ClientCredentialsTokenRequest
                {
                    ClientId = _managementId,
                    ClientSecret = _managementSecret,
                    Audience = $"https://{_managementDomain}/api/v2/"
                };

                var tokenResponse = await authenticationApiClient.GetTokenAsync(tokenRequest);
                token = tokenResponse.AccessToken;
                tokenExpiration = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);
                tokenCreationTime = DateTime.UtcNow;

                // Save the new token, expiration time, and creation time to the CSV file
                File.WriteAllText(filePath, $"{token},{tokenExpiration:o},{tokenCreationTime:o}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to obtain management token.", ex);
            }
        }

        return token;
    }
}