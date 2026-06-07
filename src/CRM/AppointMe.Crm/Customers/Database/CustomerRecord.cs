namespace AppointMe.Crm.Customers.Database;

internal sealed class CustomerRecord
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string? LastName { get; init; }
    public required DateOnly? DateOfBirth { get; init; }
    public required string? Gender { get; init; }
    public required string? Email { get; init; }
    public required DateTimeOffset RegistrationDate { get; init; }
}

internal static class CustomerRecordExtensions
{
    extension(CustomerRecord record)
    {
        internal CustomerDto ToDto()
        {
            var personName = new PersonName(record.FirstName, record.LastName);
            return new CustomerDto
            {
                Id = record.Id,
                FirstName = personName.FirstName,
                LastName = personName.LastName,
                FullName = personName.FullName,
                Initials = personName.Initials,
                DateOfBirth = record.DateOfBirth,
                Gender = Gender.ParseOrNull(record.Gender),
                Email = record.Email,
                RegistrationDate = record.RegistrationDate
            };
        }
    }
}
