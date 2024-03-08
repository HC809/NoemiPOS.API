namespace NoemiPOS.Domain.Shared;
public record Address(
    string Country,
    string State,
    string City,
    string Street,
    string PostalCode);
