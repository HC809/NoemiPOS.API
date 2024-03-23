using FluentValidation;

namespace NoemiPOS.Application.Tenants.RegisterTenant;
internal class RegisterTenantCommandValidator : AbstractValidator<RegisterTenantCommand>
{
    public RegisterTenantCommandValidator()
    {
        RuleFor(tenant => tenant.Email).EmailAddress().WithMessage("No es una dirección de correo electrónico válida"); ;
        RuleFor(tenant => tenant.Description).NotEmpty();
        When(tenant => tenant.ManagementNote != null, () =>
        {
            RuleFor(tenant => tenant.ManagementNote)
                .MinimumLength(10)
                .WithMessage("La nota de gestión debe tener al menos 10 caracteres")
                .MaximumLength(2000)
                .WithMessage("La nota de gestión no debe superar los 2,000 caracteres");
        });
    }
}