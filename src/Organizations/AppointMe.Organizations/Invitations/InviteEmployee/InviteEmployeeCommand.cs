namespace AppointMe.Organizations.Invitations.InviteEmployee;

public sealed class InviteEmployeeCommand
{
    public required string Email { get; init; }
    public required Role[] Roles { get; init; }
}
