using AppointMe.Organizations.Contracts.Invitations.Events;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Invitations.AcceptInvitation;

public static class AcceptInvitation
{
    extension(EmployeeInvitation invitation)
    {
        public void Accept(UserId userId, DateTimeOffset now)
        {
            if (invitation.Status != InvitationStatus.Pending)
            {
                throw new ValidationException("Invitation is not pending.");
            }

            if (invitation.ExpiresAt < now)
            {
                throw new ValidationException("Invitation has expired.");
            }

            invitation.Status = InvitationStatus.Accepted;
            invitation.AcceptedAt = now;
            invitation.Raise(new EmployeeInvitationAccepted(invitation.Id.Value, invitation.CompanyId.Value, userId.Value));
        }
    }
}
