namespace NoemiPOS.Domain.Tenants;

public record TenantRtn
{
    public string? Value { get; }

    public TenantRtn(string value)
    {
        if (value is not null && !IsTaxRegistryNumberValid(value))
            throw new ArgumentException("El formato del RTN no es válido.", nameof(value));

        Value = value;
    }

    private bool IsTaxRegistryNumberValid(string webSiteUrl)
    {
        // Implementación de una validación básica de formato de RTN
        // Puedes usar expresiones regulares o alguna otra lógica de validación
        return webSiteUrl.Contains(""); // Ejemplo muy básico
    }
}
