using AppointMe.Organizations.Contracts.Invitations.Events;

namespace AppointMe.Organizations.Invitations.CancelInvitation;

public static class CancelInvitation
{
    extension(EmployeeInvitation invitation)
    {
        public void Cancel()
        {
            if (invitation.Status != InvitationStatus.Pending)
            {
                throw new ValidationException("Only pending invitations can be cancelled.");
            }

            invitation.Raise(new EmployeeInvitationCancelled(invitation.Id.Value, invitation.CompanyId.Value));
        }
    }
}
