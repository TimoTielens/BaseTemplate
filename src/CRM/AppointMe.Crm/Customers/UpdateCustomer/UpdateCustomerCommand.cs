namespace AppointMe.Crm.Customers.UpdateCustomer;

public sealed class UpdateCustomerCommand
{
    public required Guid CustomerId { get; init; }
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public string? Gender { get; init; }
    public string? Email { get; init; }
}
