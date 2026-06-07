using AppointMe.Shared.Authorization.Permissions;

namespace AppointMe.Organizations.Invitations;

public static class InvitationPermissions
{
    public static readonly Permission Resend = new("invitations", "resend");
    public static readonly Permission Cancel = new("invitations", "cancel");
}
