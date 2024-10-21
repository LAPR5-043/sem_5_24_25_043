using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Amazon.Runtime;

namespace sem_5_24_25_043;

public class AuthService
{
    private readonly AmazonCognitoIdentityProviderClient _provider;
    private readonly string _clientId;
    private readonly string _userPoolId;

    public AuthService(IConfiguration configuration)
    {
        _clientId = configuration["AWS:Cognito:ClientId"];
        _userPoolId = configuration["AWS:Cognito:UserPoolId"];
        var credentials = new BasicAWSCredentials(configuration["AWS:AccessKey"], configuration["AWS:SecretKey"]);
        _provider = new AmazonCognitoIdentityProviderClient(credentials, Amazon.RegionEndpoint.USEast1);
    }

    public async Task<string> SignInAsync(string username, string password)
    {
        var request = new InitiateAuthRequest
        {
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            ClientId = _clientId,
            AuthParameters = new Dictionary<string, string>
            {
                { "USERNAME", username },
                { "PASSWORD", password }
            }
        };

        var response = await _provider.InitiateAuthAsync(request);
        return response.AuthenticationResult.IdToken;
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
    public async Task<bool> RegisterNewPatientAsync(string? email, string? patientEmail, string? password)
    {
        var signUpRequest = new AdminCreateUserRequest
        {
            UserPoolId = _userPoolId,
            Username = email,
            TemporaryPassword = password,
            UserAttributes = new List<AttributeType>
            {
                new AttributeType { Name = "custom:PersonalMail", Value = patientEmail },
                new AttributeType { Name = "email", Value = email },
                new AttributeType { Name = "email_verified", Value = "false" }
            },
            DesiredDeliveryMediums = new List<string> { "EMAIL" },
            MessageAction = "SUPPRESS"
        };
        
        Console.WriteLine("Creating user: " + email);

        var response = await _provider.AdminCreateUserAsync(signUpRequest);

        Console.WriteLine("Created user: " + response.User.Username); // Não está a cehgar aqui
        
        // Disable the user immediately after creation
        var adminDisableUserRequest = new AdminDisableUserRequest
        {
            UserPoolId = _userPoolId,
            Username = email
        };

        await _provider.AdminDisableUserAsync(adminDisableUserRequest);

        Console.WriteLine("Disabled user: " + response.User.Username);

        return response.User != null;
    }
}