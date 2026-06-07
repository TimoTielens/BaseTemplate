namespace AppointMe.Booking.ServiceProviders;

public sealed record ServiceProviderDto
{
    public required Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? FullName { get; init; }
}
