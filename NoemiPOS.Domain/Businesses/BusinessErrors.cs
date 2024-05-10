using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Domain.Businesses;
public static class BusinessErrors
{
    public static readonly Error NotFound = new(
        "Business.NotFound",
        "No se ha encontrado ningún negocio con el ID especificado.");

    public static readonly Error ExistsRtn = new(
        "Business.ExistsRtn",
        "Ya existe un negocio con el RTN especificado.");

    public static readonly Error ExistsEmail = new(
        "Business.ExistsEmail",
        "Ya existe un negocio con el correo electrónico especificado.");

    public static readonly Error ExistsName = new(
        "Business.ExistsName",
        "Ya existe un negocio con el nombre especificado.");

    public static readonly Error InvalidBusinessType = new(
        "Business.InvalidBusinessType",
        "No existe un tipo de negocio con el nombre especificado.");
}
