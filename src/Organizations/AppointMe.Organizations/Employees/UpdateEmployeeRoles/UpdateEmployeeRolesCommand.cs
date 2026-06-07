namespace AppointMe.Organizations.Employees.UpdateEmployeeRoles;

public sealed class UpdateEmployeeRolesCommand
{
    public required Guid Id { get; init; }
    public required Role[] Roles { get; init; }
}
