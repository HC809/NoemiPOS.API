using FluentValidation;

namespace NoemiPOS.Application.Businesses.RegisterBusiness;
internal class RegisterBusinessCommandValidator : AbstractValidator<RegisterBusinessCommand>
{
    private string RequiredErrorMessage = "El campo '{PropertyName}' es obligatorio";
    private string OnlyDigitsErrorMessage = "El '{PropertyName}' solo debe contener dígitos";

    public RegisterBusinessCommandValidator()
    {
        RuleFor(x => x.TenantId)
           .NotEmpty().WithMessage(RequiredErrorMessage)
           .Must(BeAValidGuid).WithMessage("El '{PropertyName}' no es un GUID válido")
           .WithName("Id del Inquilino");

        RuleFor(x => x.Name).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(3).WithMessage("El '{PropertyName}' debe ser de al menos 3 caracteres").
            MaximumLength(250).WithMessage("La '{PropertyName}' no debe superar los 250 caracteres").
            WithName("Nombre del Negocio");

        RuleFor(x => x.Description).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(10).WithMessage("La '{PropertyName}' debe tener al menos 10 caracteres").
            MaximumLength(2000).WithMessage("La '{PropertyName}' no debe superar los 2,000 caracteres").
            WithName("Descripción");

        RuleFor(x => x.Rtn).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            Length(14).WithMessage("El '{PropertyName}' debe tener 13 dígitos").
            Matches("^[0-9]+$").WithMessage(OnlyDigitsErrorMessage).
            WithName("DNI");

        RuleFor(x => x.Email).
             Cascade(CascadeMode.Stop).
             NotEmpty().WithMessage(RequiredErrorMessage).
             EmailAddress().WithMessage("El '{PropertyName}' no es válido").
             WithName("Correo Electrónico");

        RuleFor(x => x.Phone).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            Matches(@"^\d{8}$").WithMessage("El '{PropertyName}' debe tener 8 dígitos").
            WithName("Número de Teléfono");

        When(x => !string.IsNullOrEmpty(x.SecondaryPhone), () =>
        {
            RuleFor(x => x.SecondaryPhone).
                Cascade(CascadeMode.Stop).
                Matches(@"^\d{8}$").WithMessage("El '{PropertyName}' debe tener 8 dígitos").
                WithName("Número de Teléfono Secundario");
        });

        RuleFor(x => x.Country).NotEmpty().WithMessage(RequiredErrorMessage).WithName("País");
        RuleFor(x => x.State).NotEmpty().WithMessage(RequiredErrorMessage).WithName("Departamento");
        RuleFor(x => x.City).NotEmpty().WithMessage(RequiredErrorMessage).WithName("Ciudad");
        RuleFor(x => x.Street).NotEmpty().WithMessage(RequiredErrorMessage).WithName("Calle");
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage(RequiredErrorMessage).WithName("Código Postal");

        RuleFor(x => x.ManagementNote).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(10).WithMessage("La '{PropertyName}' debe tener al menos 10 caracteres").
            MaximumLength(2000).WithMessage("La '{PropertyName}' no debe superar los 2,000 caracteres").
            WithName("Nota de Gestión");

        When(x => !string.IsNullOrEmpty(x.WebSiteUrl), () =>
            {
                RuleFor(x => x.WebSiteUrl).
                    Cascade(CascadeMode.Stop).
                    Must(BeAValidUrl).WithMessage("El '{PropertyName}' no es válido").
                    WithName("URL del Sitio Web");
            });
    }

    private bool BeAValidGuid(Guid tenantId) => tenantId != Guid.Empty;

    private bool BeAValidUrl(string url)
        => Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
}
