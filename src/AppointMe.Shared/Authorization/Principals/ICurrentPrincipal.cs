namespace AppointMe.Shared.Authorization.Principals;

public interface ICurrentPrincipal
{
    IPrincipal? Principal { get; }
    IDisposable Change(IPrincipal principal);
}
