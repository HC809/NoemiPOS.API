namespace NoemiPOS.Domain.Businesses;

public record WebSiteUrl
{
    public string Value { get; }

    public WebSiteUrl(string value)
    {
        if (!IsWebSiteUrlValid(value))
            throw new ArgumentException("El formato de la URL no es válido.", nameof(value));

        Value = value;
    }

    private bool IsWebSiteUrlValid(string webSiteUrl)
    {
        // Implementación de una validación básica de formato de URL
        // Puedes usar expresiones regulares o alguna otra lógica de validación
        return webSiteUrl.Contains("http://"); // Ejemplo muy básico
    }
}
