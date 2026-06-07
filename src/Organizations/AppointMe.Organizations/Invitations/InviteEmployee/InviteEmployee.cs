using AppointMe.Organizations.Contracts.Invitations.Events;
using AppointMe.Shared.Users;

namespace AppointMe.Organizations.Invitations.InviteEmployee;

public static class InviteEmployee
{
    extension(EmployeeInvitation)
    {
        public static EmployeeInvitation Create(CompanyId companyId, Email email, IEnumerable<Role> roles,
            UserId invitedBy, DateTimeOffset now)
        {
            var distinctRoles = roles.Distinct().ToList();
            if (distinctRoles.Count == 0)
            {
                throw new ValidationException("At least one role is required.");
            }

            var invitation = new EmployeeInvitation
            {
                Id = new EmployeeInvitationId(NewId()),
                CompanyId = companyId,
                Email = email,
                Roles = distinctRoles,
                Status = InvitationStatus.Pending,
                ExpiresAt = now.AddDays(7),
                InvitedBy = invitedBy,
                InvitedAt = now,
            };
            invitation.Raise(new EmployeeInvited(invitation.Id.Value, companyId.Value, email.Value));
            return invitation;
        }
    }
}
