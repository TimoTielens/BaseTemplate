using AppointMe.Organizations.Contracts.Invitations.Events;

namespace AppointMe.Organizations.Invitations.ResendInvitation;

public static class ResendInvitation
{
    extension(EmployeeInvitation invitation)
    {
        public void Resend(DateTimeOffset now)
        {
            if (invitation.Status != InvitationStatus.Pending)
            {
                throw new ValidationException("Invitation is not pending.");
            }

            if (invitation.ExpiresAt < now)
            {
                throw new ValidationException("Invitation has expired.");
            }

            invitation.Raise(new EmployeeInvitationResent(invitation.Id.Value, invitation.CompanyId.Value, invitation.Email.Value));
        }
    }
}
