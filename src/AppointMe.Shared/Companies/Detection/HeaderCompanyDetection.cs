using Microsoft.AspNetCore.Http;

namespace AppointMe.Shared.Companies.Detection;

public class HeaderCompanyDetection(string headerName) : ICompanyDetection
{
    public CompanyId? Detect(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.TryGetValue(headerName, out var values))
        {
            return null;
        }

        var header = values.FirstOrDefault();
        return Guid.TryParse(header, out var guid) ? new CompanyId(guid) : null;
    }
}
