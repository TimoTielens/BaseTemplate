namespace AppointMe.Shared.Authorization.Principals;

public interface ICurrentPrincipalResolver
{
    ValueTask<IPrincipal> Resolve(CancellationToken cancellationToken);
}
