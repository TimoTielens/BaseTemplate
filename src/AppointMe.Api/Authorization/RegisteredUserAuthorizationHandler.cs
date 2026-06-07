using Microsoft.AspNetCore.Authorization;

namespace AppointMe.Api.Authorization;

public sealed class RegisteredUserAuthorizationHandler(
    IIdentityResolver identityResolver
) : AuthorizationHandler<RegisteredUserRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        RegisteredUserRequirement requirement)
    {
        var identity = await identityResolver.Resolve(CancellationToken.None);

        if (identity is UserIdentity)
        {
            context.Succeed(requirement);
        }
    }
}
