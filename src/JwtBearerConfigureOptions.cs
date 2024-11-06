using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace sem_5_24_25_043;


public class JwtBearerConfigureOptions(IConfiguration configuration)
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private const string ConfigurationSectionName = "JwtBearer";

    public void Configure(JwtBearerOptions options)
    {
        configuration.GetSection(ConfigurationSectionName).Bind(options);
        options.TokenValidationParameters.RoleClaimType = "custom:roles";
        
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }
}

