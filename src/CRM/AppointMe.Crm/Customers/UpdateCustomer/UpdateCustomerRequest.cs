namespace AppointMe.Crm.Customers.UpdateCustomer;

public sealed class UpdateCustomerRequest
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public string? Gender { get; init; }
    public string? Email { get; init; }
}

public static class UpdateCustomerRequestExtensions
{
    extension(UpdateCustomerRequest request)
    {
        public UpdateCustomerCommand ToCommand(Guid id) => new()
        {
            CustomerId = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            Email = request.Email
        };
    }
}
