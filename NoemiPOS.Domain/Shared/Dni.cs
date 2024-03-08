namespace NoemiPOS.Domain.Shared;
public record Dni
{
    public string Value { get; }

    public Dni(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El DNI no puede estar vacío.", nameof(value));

        //if (!IsEmailValid(value))
        //    throw new ArgumentException("El formato del DNI no es válido.", nameof(value));

        Value = value;
    }
}
