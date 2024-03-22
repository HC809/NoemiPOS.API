namespace NoemiPOS.Application.Exceptions;
public sealed record ValidationError(string PropertyName, string ErrorMessage);