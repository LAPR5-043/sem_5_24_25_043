using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;
using NuGet.Protocol;

namespace sem_5_24_25_043;

public class AuthService
{
    private readonly AmazonCognitoIdentityProviderClient _provider;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _userPoolId;

    private readonly IEmailService _emailService;
    private readonly IEncryptionEmailService _encryptionEmailService;

    public AuthService(IConfiguration configuration, IEmailService emailService, IEncryptionEmailService encryptionEmailService)
    {
        _clientId = configuration["AWS:Cognito:ClientId"];
        _clientSecret = configuration["AWS:Cognito:ClientSecret"];
        _userPoolId = configuration["AWS:Cognito:UserPoolId"];
        var credentials = new BasicAWSCredentials(configuration["AWS:AccessKey"], configuration["AWS:SecretKey"]);
        _provider = new AmazonCognitoIdentityProviderClient(credentials, Amazon.RegionEndpoint.USEast1);
        _emailService = emailService;
        _encryptionEmailService = encryptionEmailService;
    }

    public async Task<(string AccessToken, string IdToken)> SignInAsync(string email, string password)
    {
        //Console.WriteLine(_clientSecret);

        var secretHash = CalculateSecretHash(email);

        var request = new InitiateAuthRequest
        {
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            ClientId = _clientId,

            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", email },
                { "PASSWORD", password },
                { "SECRET_HASH", secretHash }

            }
        };

        try
        {
            var response = await _provider.InitiateAuthAsync(request);

            // Ensure the authentication result is not null
            if (response.AuthenticationResult == null)
            {
                // Log the response for debugging
                Console.WriteLine($"Authentication failed. Response: {response}");

                // Try to get the error message if it exists
                if (response.ResponseMetadata.Metadata.TryGetValue("message", out var errorMessage))
                {
                    throw new InvalidOperationException(errorMessage);
                }
                else
                {
                    throw new InvalidOperationException("Authentication failed, but no error message was provided.");
                }
            }

            // Return tokens if authentication was successful
            return (response.AuthenticationResult.AccessToken, response.AuthenticationResult.IdToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Authentication failed.", ex);
        }
    }

    private string CalculateSecretHash(string username)
    {
        // Create the message by concatenating the username and client ID
        var message = username + _clientId;

        // Convert the message and key to byte arrays
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);
        byte[] keyBytes = Encoding.UTF8.GetBytes(_clientSecret);

        // Generate the HMAC SHA256 hash
        using (var hmacsha256 = new HMACSHA256(keyBytes))
        {
            byte[] hashMessage = hmacsha256.ComputeHash(messageBytes);
            // Return the base64-encoded hash
            return Convert.ToBase64String(hashMessage);
        }
    }


    public async Task SignUpAsync(string username, string password, string email)
    {
        var signUpRequest = new SignUpRequest
        {
            ClientId = _clientId,
            Username = username,
            Password = password,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "email", Value = email }
            }
        };

        await _provider.SignUpAsync(signUpRequest);
    }

    public async Task ConfirmSignUpAsync(string username, string confirmationCode)
    {
        var request = new ConfirmSignUpRequest
        {
            ClientId = _clientId,
            Username = username,
            ConfirmationCode = confirmationCode
        };

        await _provider.ConfirmSignUpAsync(request);
    }
    //  public async Task GetUserGroupsByEmailAsync(string email)
    //  {
    //      var request = new AdminListGroupsForUserRequest
    //      {
    //          UserPoolId = _userPoolId,
    //          Username = email
    //      };
    //
    //      var response = await _provider.AdminListGroupsForUserAsync(request);
    //      return response.Groups.Select(g => g.GroupName);
    //  }
    public async Task<string?> GetUserEmailByTokenAsync(string authToken)
    {
        var request = new GetUserRequest
        {
            AccessToken = authToken
        };

        var response = await _provider.GetUserAsync(request);
        var emailAttribute = response.UserAttributes.FirstOrDefault(attr => attr.Name == "email");
        return emailAttribute?.Value;
    }
    public async Task<bool> RegisterNewPatientAsync(string name, string phoneNumber, string email, string patientEmail, string password)
    {

        var signUpRequest = new AdminCreateUserRequest
        {
            UserPoolId = _userPoolId,
            Username = email,
            TemporaryPassword = password,
            UserAttributes = new List<AttributeType>
            {
            new AttributeType { Name = "custom:internalEmail", Value = patientEmail },
            new AttributeType { Name = "email", Value = email },
            new AttributeType { Name = "email_verified", Value = "false" },
            new AttributeType { Name = "name", Value = name },
            new AttributeType { Name = "phone_number", Value = phoneNumber }
            },
            DesiredDeliveryMediums = new List<string> { "EMAIL" },
            MessageAction = "SUPPRESS" // Suppress default Cognito email
        };

        var response = await _provider.AdminCreateUserAsync(signUpRequest);

        // Set the password as permanent
        await _provider.AdminSetUserPasswordAsync(new AdminSetUserPasswordRequest
        {
            UserPoolId = _userPoolId,
            Username = email,
            Password = password, 
            Permanent = true
        });

        // Add user to "patient" group
        var addUserToGroupRequest = new AdminAddUserToGroupRequest
        {
            UserPoolId = _userPoolId,
            Username = email,
            GroupName = "patient"
        };

        await _provider.AdminAddUserToGroupAsync(addUserToGroupRequest);

        /*// Confirm the user
        var confirmSignUpRequest = new AdminConfirmSignUpRequest
        {
            UserPoolId = _userPoolId,
            Username = email
        };

        await _provider.AdminConfirmSignUpAsync(confirmSignUpRequest);*/


        // Disable the user immediately after creation
        var adminDisableUserRequest = new AdminDisableUserRequest
        {
            UserPoolId = _userPoolId,
            Username = email
        };

        // Disable the user immediately after creation
        await _provider.AdminDisableUserAsync(adminDisableUserRequest);

        // Send the confirmation email
        await _emailService.SendConfirmationEmail(email);
        
        return response.User != null;
    }

    public async Task<bool> ConfirmPatientEmailAsync(string encryptedEmail)
    {
        var email = _encryptionEmailService.DecryptEmail(encryptedEmail);

        try
        {
            var request = new AdminUpdateUserAttributesRequest
            {
                UserPoolId = _userPoolId,
                Username = email,
                UserAttributes = new List<AttributeType>
                {
                    new AttributeType { Name = "email_verified", Value = "true" }
                }
            };

            await _provider.AdminUpdateUserAttributesAsync(request);

            // Enable the user account after email verification
            var adminEnableUserRequest = new AdminEnableUserRequest
            {
                UserPoolId = _userPoolId,
                Username = email
            };

            await _provider.AdminEnableUserAsync(adminEnableUserRequest);

            return true;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to confirm email.", ex);
        }
    }

    public static string GetInternalEmailFromToken(HttpContext httpContext)
    {
        var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.Claims.First(claim => claim.Type == "custom:internalEmail").Value;
    }
    public static IEnumerable<string> GetGroupsFromToken(HttpContext httpContext)
    {
        var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken.Claims.Where(claim => claim.Type == "cognito:groups").Select(claim => claim.Value);
    }

}