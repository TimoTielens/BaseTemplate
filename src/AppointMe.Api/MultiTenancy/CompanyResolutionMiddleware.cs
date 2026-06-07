using AppointMe.Shared.Companies.Detection;

namespace AppointMe.Api.MultiTenancy;

public class CompanyResolutionMiddleware(RequestDelegate next, IEnumerable<ICompanyDetection> strategies)
{
    public async Task InvokeAsync(HttpContext context, ICurrentCompany currentCompany)
    {
        var companyId = DetectCompany(context);

        if (companyId is null)
        {
            await next(context);
            return;
        }

        using (currentCompany.Change(companyId.Value))
        {
            await next(context);
        }
    }

    private CompanyId? DetectCompany(HttpContext context)
    {
        foreach (var strategy in strategies)
        {
            if (strategy.Detect(context) is { } companyId)
            {
                return companyId;
            }
        }

        return null;
    }
}
