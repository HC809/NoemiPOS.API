namespace NoemiPOS.Domain.Abstractions;
public record Error(string Code, string Description)
{
    public static Error None = new(string.Empty, string.Empty);

    public static Error NullValue = new("Error.NullValue", "Null value was provided.");
}
