namespace sem_5_24_25_043.Services;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Auth0ManagementTokenService
{
    private readonly HttpClient _httpClient;
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _domain;

    public Auth0ManagementTokenService(IConfiguration configuration)
    {
        _httpClient = new HttpClient();
        _clientId = configuration["Auth0:ClientId"];
        _clientSecret = configuration["Auth0:ClientSecret"];
        _domain = configuration["Auth0:Domain"];
    }

    public async Task<string> GetManagementTokenAsync()
    {
        var requestBody = new
        {
            client_id = _clientId,
            client_secret = _clientSecret,
            audience = $"{_domain}/api/v2/",
            grant_type = "client_credentials"
        };

        var requestContent = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"https://{_domain}/oauth/token", requestContent);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Unable to retrieve Auth0 management token. Status: {response.StatusCode}");
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokenResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
        var accessToken = tokenResponse.GetProperty("access_token").GetString();

        return accessToken;
    }
}
