namespace AppointMe.Crm.Contracts.Customers;

public sealed record CustomerSnapshot(
    Guid CustomerId,
    Guid CompanyId,
    string FirstName,
    string? LastName,
    DateOnly? DateOfBirth,
    string? Email);
