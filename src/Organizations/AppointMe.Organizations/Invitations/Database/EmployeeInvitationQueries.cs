namespace AppointMe.Organizations.Invitations.Database;

internal static class EmployeeInvitationQueries
{
    extension(IQueryable<EmployeeInvitation> employeeInvitations)
    {
        public async Task<EmployeeInvitation> LoadAsync(EmployeeInvitationId id, CompanyId companyId,
            CancellationToken cancellationToken)
        {
            var invitation = await employeeInvitations.SingleOrDefaultAsync(invitation =>
                invitation.Id == id && invitation.CompanyId == companyId, cancellationToken);

            return invitation ?? throw new NotFoundException($"Employee invitation with id='{id.Value}' not found");
        }

        public async Task<EmployeeInvitation> LoadForRecipientAsync(EmployeeInvitationId id, Email email,
            CancellationToken cancellationToken)
        {
            var invitation = await employeeInvitations
                .IgnoreQueryFilters([EmployeeInvitationFilters.CompanyId])
                .SingleOrDefaultAsync(invitation => invitation.Id == id
                                                    && invitation.Email == email, cancellationToken);

            return invitation ?? throw new NotFoundException($"Employee invitation with id='{id.Value}' not found");
        }
    }
}
