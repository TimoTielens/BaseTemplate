namespace AppointMe.Shared.Companies;

public interface ICurrentCompany
{
    CompanyId? CompanyId { get; }
    IDisposable Change(CompanyId companyId);
}
