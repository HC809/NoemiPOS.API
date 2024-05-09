using FluentValidation;

namespace NoemiPOS.Application.Users.RegisterUser;
internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private string RequiredErrorMessage = "El campo '{PropertyName}' es obligatorio";
    private string OnlyDigitsErrorMessage = "El '{PropertyName}' solo debe contener dígitos";

    public RegisterUserCommandValidator()
    {
            
    }
}
