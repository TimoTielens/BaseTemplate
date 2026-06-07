namespace AppointMe.Shared.Domain.Errors;

public class NotFoundException(string message, string? code = null) : DomainException(message, code);
