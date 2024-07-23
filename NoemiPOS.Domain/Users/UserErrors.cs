using NoemiPOS.Domain.Abstractions;

namespace NoemiPOS.Domain.Users;
public static class UserErrors
{
    public static readonly Error NotFound = new(
        "User.NotFound",
        "No se ha encontrado ningún usuario con el ID especificado.");

    public static readonly Error ExistsDni = new(
        "User.ExistsDni",
        "Ya existe un usuario con el DNI especificado.");

    public static readonly Error ExistsEmail = new(
        "User.ExistsEmail",
        "Ya existe un usuario con el correo electrónico especificado.");

    public static readonly Error ExistsUsername = new(
        "User.ExistsUsername",
        "Ya existe un usuario con el nombre de usuario especificado.");

    public static readonly Error InvalidCredentials = new(
        "User.InvalidCredentials",
        "Usuario o contraseña incorrectos.");
}
