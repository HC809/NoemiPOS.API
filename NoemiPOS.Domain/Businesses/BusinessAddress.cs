namespace NoemiPOS.Domain.Businesses;
public record BusinessAddress(
    string Country,
    string State,
    string City,
    string Street,
    string PostalCode);
