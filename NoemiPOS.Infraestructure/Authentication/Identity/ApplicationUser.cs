using Microsoft.AspNetCore.Identity;
using NoemiPOS.Domain.Users;

namespace NoemiPOS.Infraestructure.Authentication.Identity;
public sealed class ApplicationUser : IdentityUser
{
    public Role Role { get; set; }
}
