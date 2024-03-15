namespace NoemiPOS.Domain.Shared;

public record SecondaryPhoneNumber
{
    public string? Value { get; }

    public SecondaryPhoneNumber(string value)
    {
        if (value is not null && !IsPhoneNumberValid(value))
            throw new ArgumentException("El formato del número de teléfono no es válido.", nameof(value));

        Value = value;
    }

    private bool IsPhoneNumberValid(string phoneNumber)
    {
        // Implementación de una validación básica de formato de número de teléfono para Honduras
        // Puedes usar expresiones regulares o alguna otra lógica de validación
        return phoneNumber.Contains("+504"); // Ejemplo muy básico
    }
}
