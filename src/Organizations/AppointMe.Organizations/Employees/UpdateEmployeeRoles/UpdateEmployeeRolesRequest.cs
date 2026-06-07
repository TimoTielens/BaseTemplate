namespace AppointMe.Organizations.Employees.UpdateEmployeeRoles;

public sealed class UpdateEmployeeRolesRequest
{
    public required Role[] Roles { get; init; }
}
