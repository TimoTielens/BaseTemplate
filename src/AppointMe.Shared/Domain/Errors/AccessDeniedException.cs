namespace AppointMe.Shared.Domain.Errors;

public class AccessDeniedException(string message) : DomainException(message);
