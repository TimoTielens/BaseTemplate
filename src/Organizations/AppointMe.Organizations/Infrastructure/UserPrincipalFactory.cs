using AppointMe.Organizations.Database;
using AppointMe.Organizations.Settings.Permissions.Infrastructure;

namespace AppointMe.Organizations.Infrastructure;

public class UserPrincipalFactory(
    OrganizationsDbContext dbContext,
    ICurrentCompany currentCompany,
    PermissionResolver permissionResolver,
    RolePermissionOverridesCache permissionOverridesCache)
{
    public async ValueTask<UserPrincipal> Create(UserIdentity user, CancellationToken cancellationToken)
    {
        var companyId = currentCompany.CompanyId
                        ?? throw new AccessDeniedException("Active company was not specified.");

        var roles = await dbContext.Employees
            .Where(employee => employee.UserId == user.Id)
            .Select(employee => employee.Roles)
            .SingleOrDefaultAsync(cancellationToken);

        if (roles is null || roles.Count == 0)
        {
            throw new AccessDeniedException("User is not authorized to access requested company.");
        }

        var permissionOverrides = await permissionOverridesCache.GetAsync(companyId, cancellationToken);

        var userRoles = roles.ToHashSet();
        return new UserPrincipal(
            CompanyId: companyId,
            Roles: userRoles,
            Permissions: permissionResolver.Resolve(userRoles, permissionOverrides));
    }
}
