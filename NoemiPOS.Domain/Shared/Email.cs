namespace NoemiPOS.Domain.Shared;
public record Email
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El email no puede estar vacío.", nameof(value));

        if (!IsEmailValid(value))
            throw new ArgumentException("El formato del email no es válido.", nameof(value));

        Value = value;
    }

    private bool IsEmailValid(string email)
    {
        // Implementación de una validación básica de formato de email
        // Puedes usar expresiones regulares o alguna otra lógica de validación
        return email.Contains("@"); // Ejemplo muy básico
    }
}
