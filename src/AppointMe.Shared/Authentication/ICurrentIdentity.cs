namespace AppointMe.Shared.Authentication;

public interface ICurrentIdentity
{
    IIdentity? Identity { get; }
    IDisposable Change(IIdentity identity);
}
