using Microsoft.AspNetCore.Http;

namespace AppointMe.Shared.Companies.Detection;

public interface ICompanyDetection
{
    CompanyId? Detect(HttpContext httpContext);
}
