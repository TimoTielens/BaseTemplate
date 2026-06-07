namespace AppointMe.Crm.Customers.RegisterCustomer;

public sealed class RegisterCustomerRequest
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public string? Gender { get; init; }
    public string? Email { get; init; }
}

public static class RegisterCustomerRequestExtensions
{
    extension(RegisterCustomerRequest request)
    {
        public RegisterCustomerCommand ToCommand()
        {
            return new RegisterCustomerCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Email = request.Email
            };
        }
    }
}
