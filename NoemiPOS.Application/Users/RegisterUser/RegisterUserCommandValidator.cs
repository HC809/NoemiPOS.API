using FluentValidation;

namespace NoemiPOS.Application.Users.RegisterUser;
internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private string RequiredErrorMessage = "El campo '{PropertyName}' es obligatorio";
    private string OnlyDigitsErrorMessage = "El '{PropertyName}' solo debe contener dígitos";

    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.BusinessId)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Must(BeAValidGuid).WithMessage("El '{PropertyName}' no es un GUID válido")
            .WithName("Id del Negocio");

        RuleFor(x => x.FirstName).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(3).WithMessage("El '{PropertyName}' debe ser de al menos 3 caracteres").
            MaximumLength(250).WithMessage("La '{PropertyName}' no debe superar los 250 caracteres").
            WithName("Nombre del Usuario");

        RuleFor(x => x.LastName).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(3).WithMessage("El '{PropertyName}' debe ser de al menos 3 caracteres").
            MaximumLength(250).WithMessage("La '{PropertyName}' no debe superar los 250 caracteres").
            WithName("Apellido del Usuario");

        RuleFor(x => x.Email).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            EmailAddress().WithMessage("El '{PropertyName}' no es válido").
            WithName("Correo Electrónico");

        RuleFor(x => x.Dni).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            Length(13).WithMessage("El '{PropertyName}' debe tener 13 dígitos").
            Matches("^[0-9]+$").WithMessage(OnlyDigitsErrorMessage).
            WithName("DNI");

        RuleFor(x => x.PhoneNumber).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            Matches(@"^\d{8}$").WithMessage("El '{PropertyName}' debe tener 8 dígitos").
            WithName("Número de Teléfono");

        RuleFor(x => x.Username).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(6).WithMessage("El '{PropertyName}' debe ser de al menos 6 caracteres").
            WithName("Nombre de Usuario");

        RuleFor(x => x.Password).
            Cascade(CascadeMode.Stop).
            NotEmpty().WithMessage(RequiredErrorMessage).
            MinimumLength(6).WithMessage("La '{PropertyName}' debe ser de al menos 6 caracteres").
            WithName("Contraseña");
    }

    private bool BeAValidGuid(Guid tenantId) => tenantId != Guid.Empty;

    private bool BeAValidUrl(string url)
        => Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
}
