namespace NoemiPOS.Domain.Tenants;

public record TaxRegistryNumber
{
    public string Value { get; }

    public TaxRegistryNumber(string value)
    {
        if (!IsTaxRegistryNumberValid(value))
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
