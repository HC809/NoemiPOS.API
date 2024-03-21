using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Domain.Tenants;
public static class TenantErrors
{
    public static readonly Error NotFound = new(
    "Tenant.NotFound",
    "No se ha encontrado ningún inquilino con el ID especificado.");
}
