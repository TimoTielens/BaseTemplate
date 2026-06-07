
namespace AppointMe.Crm.Customers;

public sealed record Customer : AggregateRoot
{
    public required CustomerId Id { get; init; }
    public required CompanyId CompanyId { get; init; }
    public required PersonName Name { get; set; }
    public required DateOfBirth? DateOfBirth { get; set; }
    public required Gender? Gender { get; set; }
    public required Email? Email { get; set; }
    public required DateTimeOffset RegistrationDate { get; init; }
    public required bool IsDeleted { get; set; }
}
