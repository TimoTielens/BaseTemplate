namespace AppointMe.Booking.Appointments;

public sealed record AppointmentDto
{
    public required Guid Id { get; init; }
    public required DateTimeOffset Start { get; init; }
    public required DateTimeOffset End { get; init; }
    public required AppointmentStatus Status { get; init; }
    public required Guid ProviderId { get; init; }
    public string? ProviderFirstName { get; init; }
    public string? ProviderLastName { get; init; }
    public string? ProviderName { get; init; }
    public string? ProviderInitials { get; init; }
    public required Guid AttendeeId { get; init; }
    public string? AttendeeFirstName { get; init; }
    public string? AttendeeLastName { get; init; }
    public string? AttendeeName { get; init; }
    public string? AttendeeInitials { get; init; }
    public string? Notes { get; init; }
    public required DateTimeOffset ScheduledAt { get; init; }
}
