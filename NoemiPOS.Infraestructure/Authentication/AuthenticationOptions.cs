namespace NoemiPOS.Infraestructure.Authentication;
public sealed class AuthenticationOptions
{
    public string Audience { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
}
