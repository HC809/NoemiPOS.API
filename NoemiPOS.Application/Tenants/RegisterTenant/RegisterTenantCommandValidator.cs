using FluentValidation;
using NoemiPOS.Application.Tenants.RegisterTenant;

namespace NoemiPOS.Application.xs.Registerx;
internal class RegisterTenantCommandValidator : AbstractValidator<RegisterTenantCommand>
{
    private string RequiredErrorMessage = "El campo '{PropertyName}' es obligatorio";
    private string OnlyDigitsErrorMessage = "El '{PropertyName}' solo debe contener dígitos";

    public RegisterTenantCommandValidator()
    {
        RuleFor(x => x.FullName).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(12).WithMessage("El nombre completo debe ser de al menos 12 caracteres").
            MaximumLength(250).WithMessage("La nombre completo no debe superar los 250 caracteres").
            WithName("Nombre Completo");

        RuleFor(x => x.Email).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            EmailAddress().WithMessage("No es una dirección de correo electrónico válida").
            WithName("Correo Electrónico");

        RuleFor(x => x.Dni).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            Length(13).WithMessage("El '{PropertyName}' debe tener 13 dígitos").
            Matches("^[0-9]+$").WithMessage(OnlyDigitsErrorMessage).
            WithName("DNI");

        RuleFor(x => x.Rtn).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            Length(14).WithMessage("El '{PropertyName}' debe tener 14 dígitos").
            Matches("^[0-9]+$").WithMessage(OnlyDigitsErrorMessage).
            WithName("RTN");

        RuleFor(x => x.Phone).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            Length(8).WithMessage("El '{PropertyName}' debe tener 8 dígitos").
            Matches("^[0-9]+$").WithMessage(OnlyDigitsErrorMessage).
            WithName("Número de Teléfono");

        When(x => !string.IsNullOrEmpty(x.SecondaryPhone), () =>
        {
            RuleFor(x => x.SecondaryPhone).
                Cascade(CascadeMode.Stop).
                Length(8).WithMessage("El '{PropertyName}' debe tener 8 dígitos").
                Matches("^[0-9]+$").WithMessage(OnlyDigitsErrorMessage).
                WithName("Número de Teléfono Secundario");
        });

        RuleFor(x => x.Country).NotEmpty().WithMessage(RequiredErrorMessage).WithName("País");
        RuleFor(x => x.State).NotEmpty().WithMessage(RequiredErrorMessage).WithName("Departamento");
        RuleFor(x => x.City).NotEmpty().WithMessage(RequiredErrorMessage).WithName("Ciudad");

        RuleFor(x => x.Description).
            MinimumLength(10).WithMessage("La descripción debe tener al menos 10 caracteres").
            MaximumLength(2000).WithMessage("La descripción no debe superar los 2,000 caracteres");

        RuleFor(x => x.ManagementNote).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(10).WithMessage("La '{PropertyName}' debe tener al menos 10 caracteres").
            MaximumLength(2000).WithMessage("La '{PropertyName}' no debe superar los 2,000 caracteres").
            WithName("Nota de Gestión");
    }
}