namespace AppointMe.Crm.Customers;

public sealed class CustomerDto
{
    public required Guid Id { get; init; }
    public required string FullName { get; init; }
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public required string Initials { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public Gender? Gender { get; init; }
    public string? Email { get; init; }
    public required DateTimeOffset RegistrationDate { get; init; }
}
