namespace AppointMe.Shared.Domain.Errors;

public class ValidationException(string message, string? code = null) : DomainException(message, code);
