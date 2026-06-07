namespace AppointMe.Shared.Domain.Errors;

public class ConflictException(string message, string? code = null) : DomainException(message, code);
