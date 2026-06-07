namespace AppointMe.Organizations.Invitations.InviteEmployee;

public sealed class InviteEmployeeRequest
{
    public required string Email { get; init; }
    public required Role[] Roles { get; init; }
}
