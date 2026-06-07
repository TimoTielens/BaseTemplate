namespace AppointMe.Shared.Domain.Errors;

public abstract class DomainException(string message, string? code = null) : Exception(message)
{
    public string? Code { get; } = code;
}
