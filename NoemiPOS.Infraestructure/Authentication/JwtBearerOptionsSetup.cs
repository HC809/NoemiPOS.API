using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace NoemiPOS.Infraestructure.Authentication;
internal sealed class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly AuthenticationOptions _authenticationOptions;

    public JwtBearerOptionsSetup(AuthenticationOptions authenticationOptions)
    {
        _authenticationOptions = authenticationOptions;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        options.Audience = _authenticationOptions.Audience;
        // Configura el Audience y el Issuer
        options.Audience = _authenticationOptions.Audience;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = _authenticationOptions.Issuer,

            // Establece la SymmetricSecurityKey
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationOptions.Key)),

            // Valida la firma del token
            ValidateIssuerSigningKey = true,

            // Valida que el Issuer del token sea correcto
            ValidateIssuer = true,

            // Valida que el Audience del token sea correcto
            ValidateAudience = true,

            // Valida la expiración del token
            ValidateLifetime = true,

            // Podrías configurar también un "clock skew" (margen de tiempo) si es necesario
            ClockSkew = TimeSpan.Zero
        };
    }
}
