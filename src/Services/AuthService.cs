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
}