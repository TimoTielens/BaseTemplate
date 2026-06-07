namespace AppointMe.Crm.Customers.RegisterCustomer;

public sealed class RegisterCustomerCommand
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public string? Gender { get; init; }
    public string? Email { get; init; }
}
