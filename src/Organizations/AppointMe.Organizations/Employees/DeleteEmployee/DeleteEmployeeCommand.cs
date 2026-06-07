namespace AppointMe.Organizations.Employees.DeleteEmployee;

public sealed class DeleteEmployeeCommand
{
    public required Guid Id { get; init; }
}
