namespace AppointMe.Organizations.Contracts.Companies;

public sealed record CompanySnapshot(
    Guid CompanyId,
    string Name,
    string TimeZone);

public interface ICompanyRehydrationSource
{
    Task<IReadOnlyList<CompanySnapshot>> GetAll(CancellationToken cancellationToken);
}
